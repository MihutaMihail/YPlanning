###
### CERTIFICATE
###

# Generate a new SSL certificate
function New-Certificate {
    Write-Host "Generating new certificate..."

    # Prompt user to enter certificate password
    $password = Read-Host -Prompt "Enter password for the new certificate" -AsSecureString
    $passwordPlainText = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto([System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($password))
    
    # Generate a self-signed certificate
    $generateCertCommand = "dotnet dev-certs https -ep localhost.pfx -p $passwordPlainText"
    Invoke-Expression $generateCertCommand
    
    # Trust self-signed certificate
    $trustCert = "dotnet dev-certs https --trust"
    Invoke-Expression $trustCert
    
    # Ensure Docker Swarm is initialized
    Ensure-SwarmInitialized

    # Create secret from user password
    Set-CertificatePasswordSecret -password $passwordPlainText
}

# Get current certificate, if not found, return null
# ($_ is the result from coming through the pipe == the certificate)
function Get-Certificate {
    $cert = Get-ChildItem -Path "cert:\CurrentUser\My" | Where-Object { $_.Subject -like "*CN=localhost*" }

    if ($cert) {
        return $cert
    } else {
        return $null
    }
}

# Ensure that Docker Swarm Mode is initialized. Swarm Mode is needed for creating secrets
function Ensure-SwarmInitialized {
    $swarmStatus = docker info --format '{{.Swarm.LocalNodeState}}'
    if ($swarmStatus -ne 'active') {
        Write-Host "Initializing Docker Swarm..."
        
        $initSwarm = "docker swarm init"
        Invoke-Expression $initSwarm
    } else {
        Write-Host "Docker Swarm is already initialized"
    }
}

# Create Docker secret for the certificate password
function Set-CertificatePasswordSecret {
    param (
        [string]$password
    )

    $secretName = "CERT_PASSWORD"
    $secretFilePath = "./cert_password.txt"

    # Check if secret already exists
    $secretExists = docker secret ls --filter "name=$secretName" --format "{{.Name}}" | Select-String -Pattern "^$secretName$"

    if ($secretExists) {
        Write-Host "Secret '$secretName' already exists"
    } else {
        # Create new secret
        echo $password | docker secret create $secretName -
        Write-Host "Secret '$secretName' created successfully"
    }
    
    # Create file containing secret value
    $password | Set-Content -Path $secretFilePath
    Write-Host "File 'cert_password.txt' created with secret value"
}

# Remove specified file
function Remove-File {
    param (
        [string]$fileName
    )

    Remove-Item -Path $fileName -Force
    Write-Host "File '$fileName' has been removed"
}

# Remove current PFX since a new one will be added
Remove-Item -Path "./localhost.pfx" -ErrorAction SilentlyContinue

if (Get-Certificate != null) {
    do {
        $response = Read-Host -Prompt "A certificate with CN=localhost already exists. 
		Do you want to generate a new one (the existing certificate will be deleted) ? (Y/N)" -ErrorAction SilentlyContinue
    } while ($response -notin @('Y', 'N'))

    if ($response -eq "Y" -or $response -eq "y") {
        # Delete certificate
        $removeCert = "dotnet dev-certs https --clean"
        Invoke-Expression $removeCert
        
        # Generate new certificate
        New-Certificate
    } else {
        # Prompt user to enter certificate password
        $password = Read-Host -Prompt "Enter password for the existing certificate" -AsSecureString

        # Export existing certificate
        $existingCert = Get-Certificate
        Export-PfxCertificate -Cert $existingCert -FilePath "./localhost.pfx" -Password $password
        
        # Ensure Docker Swarm is initialized
        Ensure-SwarmInitialized
        
        # Create secret from user password
        $passwordPlainText = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto([System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($password))
        Set-CertificatePasswordSecret -password $passwordPlainText

        Write-Host "Using existing certificate with CN=localhost"
    }
} else {
    New-Certificate
}

###
### DOCKER
###

# Build and run API Docker image
function Run-API {
    # Build image
    $imageFullName = "yplanning:latest"

    # Check if image exists
    $imageExists = docker images -q $imageFullName

    if ($imageExists) {
        Write-Host "Image $imageFullName already exists"
    } else {
        Write-Host "Image $imageFullName does not exist. Building image..."
        $buildDockerImage = "docker build --secret id=CERT_PASSWORD,src=./cert_password.txt -t yplanning:latest ."
        Invoke-Expression $buildDockerImage
    }

    # Run container
    $containerName = "yplanning"
    $portMapping = "443:443"

    # Check if container is already running
    if (docker ps -a --format '{{.Names}}' | Select-String $containerName) {
        Write-Output "Container $containerName is already running."
    } else {
        Write-Output "Container $containerName is not running. Starting..."
        
        # Run container
        Write-Host "Running container : $containerName..."
        $runDockerContainer = "docker run -d -p $portMapping --name $containerName $imageFullName"
        Invoke-Expression $runDockerContainer
        Write-Host "Container $containerName is running on port 443"
    }
}

# Run PostgreSQL Docker image and initialize database
function Run-PostgreSQL {
    $containerName = "postgres"
    $portMapping = "5432:5432"
    $dbFile = "schema.sql"
    $dbName = "yplanning"

    # Prompt user to enter database password
    $password = Read-Host -Prompt "Enter password for the database" -AsSecureString
    $passwordPlainText = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto([System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($password))

    # Check if container is already running
    if (docker ps -a --format '{{.Names}}' | Select-String $containerName) {
        Write-Output "Container $containerName is already running."
    } else {
        Write-Output "Container $containerName is not running. Starting..."
        
        # Run container
        $runDockerContainer = "docker run --name $containerName -p $portMapping -e POSTGRES_PASSWORD=$passwordPlainText -d postgres"
        Invoke-Expression $runDockerContainer
        Write-Host "Waiting for 10 seconds for the server to start..."
        Start-Sleep -s 10
    }
    
    # Ask user if they've already set up the PostgreSQL server
    do {
        $response = Read-Host -Prompt "Type 'Y' if this is your first time executing this script ? (Y/N)" -ErrorAction SilentlyContinue
    } while ($response -notin @('Y', 'N'))

    if ($response -eq "Y" -or $response -eq "y") {
        # Copy database file into Docker
        $copyDbToDocker = "docker cp $dbFile ${containerName}:/tmp/$dbFile"
        Invoke-Expression $copyDbToDocker
        
        # Create database
        $createDatabase = "docker exec -it $containerName psql -U postgres -c 'CREATE DATABASE $dbName WITH OWNER = postgres ENCODING = ''UTF8'' LC_COLLATE = ''en_US.utf8'' LC_CTYPE = ''en_US.utf8'' LOCALE_PROVIDER = ''libc'' TABLESPACE = pg_default CONNECTION LIMIT = -1 IS_TEMPLATE = False;'"
        Invoke-Expression $createDatabase

        # Creates tables by executing script
        $exeDbFile = "docker exec -it $containerName psql -U postgres -d $dbName -f /tmp/$dbFile"
        Invoke-Expression $exeDbFile
    }
}

# Run all necessary containers
Run-API
Run-PostgreSQL

# Remove file containing secret value after creating the secret
Remove-File -fileName "cert_password.txt"