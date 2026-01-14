# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["BookHub.sln", "./"]
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["BusinessLayer/BusinessLayer.csproj", "BusinessLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Middleware/Middleware.csproj", "Middleware/"]

# Restore dependencies
RUN dotnet restore "WebAPI/WebAPI.csproj"

# Copy all source files
COPY . .

# Build the application
WORKDIR "/src/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Install curl for healthcheck
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Copy published application
COPY --from=publish /app/publish .


# Create data directory for SQLite database
RUN mkdir -p /data

EXPOSE 5000

ENTRYPOINT ["dotnet", "WebAPI.dll"]

