## Description
Description of the project

# Table of Contents
- [**Features**](#features)
- [**Prerequisites**](#prerequisites)
- [**Setup and Installation**](#setup-and-installation)
- [**Usage**](#usage)
- [**API Documentation**](#api-documentation)

## Features
- Nginx reverse proxy
- Containerized using Docker
- C# API built with ASP.NET Core
- ORM Entity Framework (EF) Core

Here's an overview of the stack
Stack : <br>
<ins>Backend Framework</ins> → ASP.NET Core <br>
<ins>Database</ins> → PostgreSQL Server <br>
<ins>ORM</ins> → Entity Framework (EF) Core <br>
<ins>Web Server</ins> → Nginx Server <br>
<ins>Deployment Platform</ins> → Docker <br>
<ins>IDE</ins> → Visual studio

## Prerequisites
- Prerequisites like installing Docker 
- Cloning the repository, what to do with it to install

## Setup and Installation
- How to build docker images 
- How to start the application  
- How to access different services like http://localhost:3000 for the api server and http://localhost:5000 for the nginx server

Here's the link to download Docker (put link)
We're using version 4.30.0

Before executing the powershell script, make sure that your Docker is set to linux containers add $ImageThatShowsThat
Make sure that Docker is opened, if not the image will not be built

First execute the installtion.ps1 powershell script
Here’s the command : **powershell -ExecutionPolicy Bypass -File .\installation.ps1** (you must be inside the solution folder for this to work)
(you’ll need to choose to either generate a new one (and delete the existing one) or just use the existing certificate)
(it will also build the docker image)

The image will be inside docker Images
$showImageRunOptionsThatIncludeAddingThe443Port
If not it does not listen to the 443 port

TO NOTE
You must have .NET SDK installed, here's the link https://dotnet.microsoft.com/en-us/download/dotnet (choose .NET 6.0)
If it doesn’t work, it might be because the certificate is not trusted (**dotnet dev-certs https --trust**) or perhaps the password that you have given is incorrect (the certificate password)
You may need to restart your browser, sometimes chrome (or perhaps another browser) may use the old (now deleted) certificate which will not work (cert invalid error)


## Usage
- Explain how to use the application
- How to call endpoint
- What to expect from these endpoint
- Maybe a high-level flow

## API Documentation
- Add "refer to **api_doc**" 
