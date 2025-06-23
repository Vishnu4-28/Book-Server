# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only project files first to leverage Docker cache
COPY E-commerce.Server/E-commerce.Server.csproj E-commerce.Server/

# Restore dependencies
WORKDIR /src/E-commerce.Server
RUN dotnet restore "E-commerce.Server.csproj"

# Copy the rest of the source code
WORKDIR /src
COPY E-commerce.Server/. E-commerce.Server/

# Build the project
WORKDIR /src/E-commerce.Server
RUN dotnet build "E-commerce.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "E-commerce.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-commerce.Server.dll"]
