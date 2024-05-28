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

Write-Host "The docker image is being built..."

# Build docker image
$buildDockerImage = "docker build --secret id=CERT_PASSWORD,src=./cert_password.txt -t yplanning:latest ."
Invoke-Expression $buildDockerImage

# Remove file containing secret value after creating the secret
Remove-File -fileName "cert_password.txt"