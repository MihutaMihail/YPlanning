$APP_SETUP_PATH = "./Dev/YPlanning/AppSetup"

# /*-----------------------------------
### CERTIFICATE
# \*-----------------------------------

function Handle-Certificate {
    if (Get-Certificate != null) {
        do {
            $response = Read-Host -Prompt ("A certificate already exists. " +
            "Do you want to generate a new one (the existing certificate will be deleted) ? (Y/N)") -ErrorAction SilentlyContinue
        } while ($response -notin @('Y', 'N'))

        # Check user response
        if ($response.ToUpper() -eq "Y") {
            Get-Certificate | Remove-Item
            New-Certificate
        } else {
            Copy-ExistingCertificate 
        }
    } else {
        New-Certificate
    }
}

# Generate a new SSL certificate
function New-Certificate {
    Write-Host "New certificate password..."
    $passwordPlainText = Read-Password -PlainTextOutput
    
    # Generate a self-signed certificate and export as localhost.pfx (Personal Information Exchange)
    Invoke-Expression "dotnet dev-certs https --export-path $APP_SETUP_PATH/localhost.pfx --password $passwordPlainText"
    
    # Trust the newly created certificate
    Invoke-Expression "dotnet dev-certs https --trust"
}

# If certificate already exists, copy it
function Copy-ExistingCertificate {
    Write-Host "Existant certificate password..."
    $password = Read-Password

    # Export existing certificate
    $existingCert = Get-Certificate
    Export-PfxCertificate -Cert $existingCert -FilePath "$APP_SETUP_PATH/localhost.pfx" -Password $password

    Write-Host "Using existing certificate with CN=localhost"
}

# Get current certificate, if not found, return null
# ($_ represents the current object in the pipeline which is the certificate)
function Get-Certificate {
    return (Get-ChildItem -Path "cert:\CurrentUser\My" | Where-Object { $_.Subject -like "*CN=localhost*" })
}

# Create Docker secret for the certificate password
<#function Set-CertificatePasswordSecret {
    param (
        [string]$password
    )
    $CERT_PASSWORD = "CERT_PASSWORD"

    # Ensure that Swarm Mode is initialized
    Ensure-SwarmInitialized

    # Add password as a environnemental variable
    [System.Environment]::SetEnvironmentVariable($CERT_PASSWORD, $password, "User")

    # Check if secret already exists
    $secretExists = docker secret ls --filter "name=$CERT_PASSWORD" --format "{{.Name}}" | Select-String -Pattern "^$CERT_PASSWORD$"
    if ($secretExists) {
        Write-Host "Secret already exists. Removing existing secret..."
        docker secret rm $CERT_PASSWORD
    }

    # Create secret
    Write-Host "Creating the secret..."
    echo $password | docker secret create $CERT_PASSWORD -
    Write-Host "Secret '$CERT_PASSWORD' created successfully"
}

# Ensure that Docker Swarm Mode is initialized (needed for creating secrets)
function Ensure-SwarmInitialized {
    $swarmStatus = docker info --format '{{.Swarm.LocalNodeState}}'
    if ($swarmStatus -ne 'active') {
        Write-Host "Initializing Docker Swarm..."
        Invoke-Expression "docker swarm init"
    } else {
        Write-Host "Docker Swarm is already initialized"
    }
}#>

# /*-----------------------------------
### API
# \*-----------------------------------

function Handle-API {
    Build-API
    Run-API
}

function Build-API {
    Write-Host "Building phase..."

    $containerName = "yplanning"
    $imageFullName = "yplanning:latest"

    # Stop and remove running containers 
    Remove-Containers -containerName $containerName

    # Remove existing image
    $imageExists = docker images -q $imageFullName
    if ($imageExists) {
        Write-Host "Removing image..."
        Invoke-Expression "docker rmi $imageFullName -f"
    } else {
        Write-Host "Image $imageFullName does not exist."
    }

    # Build image
    Write-Host "Building image..."
    Invoke-Expression "docker build -t $imageFullName $APP_SETUP_PATH/../"
}

function Run-API {
    Write-Host "Running phase..."

    $containerName = "yplanning"
    $portMapping = "443:443"

    Write-Host "Certificate password..."
    $passwordPlainText = Read-Password -PlainTextOutput

    # Run container
    Write-Host "Running API container..."
    Invoke-Expression "docker run --name $containerName -p $portMapping -e 'CERT_PASSWORD=$passwordPlainText' -d $containerName"
}

# /*-----------------------------------
### POSTGRESQL
# \*-----------------------------------

function Handle-PostgreSQL {
    Run-PostgreSQL
    Execute-PostgreSQL
}

function Run-PostgreSQL {
    Write-Host "Running phase..."

    $imageFullName = "postgres:latest"
    $containerName = "postgres"
    $portMapping = "5432:5432"

    # Stop and remove running containers
    Remove-Containers -containerName $containerName

    Write-Host "Database password..."
    $passwordPlainText = Read-Password -PlainTextOutput

    # Run container
    Write-Host "Running PostgreSQL container..."
    Invoke-Expression "docker run --name $containerName -p $portMapping -e POSTGRES_PASSWORD=$passwordPlainText -d $imageFullName"
}

function Execute-PostgreSQL {
    Write-Host "Executing SQL scripts phase..."
    Write-Host "Waiting for 15 seconds for the server to start..."
    Start-Sleep -s 15

    $containerName = "postgres"
    $dbFile = "schema.sql"
    $dbName = "yplanning"

    # Copy database file
    Write-Host "Copy database file..."
    Invoke-Expression "docker cp $APP_SETUP_PATH/$dbFile ${containerName}:/tmp/$dbFile"

    # Create database
    Write-Host "Create database..."
    Invoke-Expression "docker exec -it $containerName psql -U postgres -c 'CREATE DATABASE $dbName WITH OWNER = postgres ENCODING = ''UTF8'' LC_COLLATE = ''en_US.utf8'' LC_CTYPE = ''en_US.utf8'' LOCALE_PROVIDER = ''libc'' TABLESPACE = pg_default CONNECTION LIMIT = -1 IS_TEMPLATE = False;'"

    # Creates tables
    Write-Host "Create tables..."
    Invoke-Expression "docker exec -it $containerName psql -U postgres -d $dbName -f /tmp/$dbFile"
}

# /*-----------------------------------
### HELPER
# \*-----------------------------------

function Read-Password {
    param (
        [switch]$PlainTextOutput = $false
    )

    $password = Read-Host -Prompt "Enter password" -AsSecureString

    if ($PlainTextOutput) {
        $passwordPlainText = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto([System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($password))
        return $passwordPlainText
    } else {
        return $password
    }
}

function Remove-Containers {
    param (
        [string]$containerName
    )

    # Get all containers with the specified name
    $allContainers = docker ps -a -q --filter "name=$containerName"
    
    if ($allContainers) {
        Write-Host "Removing containers with name $containerName"
        Invoke-Expression "docker rm -f $allContainers"
    } else {
        Write-Host "No containers found with name $containerName"
    }
}


# /*-----------------------------------
### --- ENTRY POINT ---
# \*-----------------------------------

function Show-Menu {
    Clear-Host
    Write-Host "=== Setup Menu ==="
    Write-Host "WARNING : CHOOSING ANYTHING AFTER THE FULL SETUP OPTION COULD RESULT IN DATA LOSS"
    Write-Host "1. FULL SETUP (RECOMMENDED FOR FIRST USE)"
    Write-Host "2. Certificate"
    Write-Host "3. API"
    Write-Host "4. PostgreSQL"
    Write-Host "Q. Quit"
    Write-Host "============="
}

do {
    Show-Menu
    $choice = Read-Host "Please enter your choice"
    switch ($choice.ToUpper()) {
        '1' {
            Handle-Certificate
            Handle-API
            Handle-PostgreSQL
            Pause
        }
        '2' {
            Handle-Certificate
            Pause
        }
        '3' {
            Handle-API
            Pause
        }
        '4' {
            Handle-PostgreSQL
            Pause
        }
        'Q' {
            Write-Host "Exiting..."
            break
        }
        default {
            Write-Host "Invalid choice. Please try again."
            Pause
        }
    }
} while ($choice -ne 'Q')
