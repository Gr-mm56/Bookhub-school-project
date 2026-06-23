# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["BookHub.sln", "./"]
COPY ["WebMVC/WebMVC.csproj", "WebMVC/"]
COPY ["BusinessLayer/BusinessLayer.csproj", "BusinessLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Middleware/Middleware.csproj", "Middleware/"]

# Restore dependencies
RUN dotnet restore "WebMVC/WebMVC.csproj"

# Copy all source files
COPY . .

# Build the application
WORKDIR "/src/WebMVC"
RUN dotnet build "WebMVC.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "WebMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Explicitly copy assets to publish output
WORKDIR /src
RUN ls -la && \
    if [ -d "assets" ]; then \
        echo "Found assets folder, copying..."; \
        cp -r assets /app/publish/wwwroot/; \
        ls -la /app/publish/wwwroot/; \
    else \
        echo "Assets folder not found at /src/assets"; \
    fi

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Install curl for healthcheck
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Copy published application
COPY --from=publish /app/publish .


# Create data directory for SQLite database
RUN mkdir -p /data

EXPOSE 5170

ENTRYPOINT ["dotnet", "WebMVC.dll"]

