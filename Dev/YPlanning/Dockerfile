# .NET app is being built
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["YPlanning.csproj", "."]
RUN dotnet restore "./YPlanning.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./YPlanning.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish built .NET app for deployment
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./YPlanning.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Create final runtime image for app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy certificate file into container
COPY ["localhost.pfx", "/app/localhost.pfx"]

# Set environment variables for the certificate
ENV CERT_PATH="/app/localhost.pfx"

# Add Docker secrets into container
RUN --mount=type=secret,id=CERT_PASSWORD \
    cat /run/secrets/CERT_PASSWORD > /app/cert_password.txt

# Copy published application from publish stage to current dir
COPY --from=publish /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "YPlanning.dll"]
