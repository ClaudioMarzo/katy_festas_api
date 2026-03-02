# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy project files
COPY src/KatyFestas.API/KatyFestas.API.csproj ./src/KatyFestas.API/
COPY src/KatyFestas.Application/KatyFestas.Application.csproj ./src/KatyFestas.Application/
COPY src/KatyFestas.Domain/KatyFestas.Domain.csproj ./src/KatyFestas.Domain/
COPY src/KatyFestas.Infrastructure/KatyFestas.Infrastructure.csproj ./src/KatyFestas.Infrastructure/

# Restore dependencies
WORKDIR /app/src/KatyFestas.API
RUN dotnet restore

# Copy everything else and build
WORKDIR /app
COPY src/ ./src/
WORKDIR /app/src/KatyFestas.API
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080

# Set environment to Development (QAS)
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "KatyFestas.API.dll"]
