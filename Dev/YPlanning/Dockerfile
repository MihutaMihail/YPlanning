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

# Setup certificate for HTTPS
COPY ["./AppSetup/localhost.pfx", "/app/localhost.pfx"]
ENV CERT_PATH="/app/localhost.pfx"

# Copy published application from publish stage to current dir
COPY --from=publish /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "YPlanning.dll"]
