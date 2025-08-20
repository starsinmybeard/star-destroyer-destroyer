#!/bin/bash

echo "🚀 Star Destroyer - Starship Manager Setup Script"
echo "=================================================="

# Check if Docker is running
if ! docker info > /dev/null 2>&1; then
    echo "❌ Docker is not running. Please start Docker Desktop and try again."
    exit 1
fi

echo "✅ Docker is running"

# Check if SQL Server container exists
if [ "$(docker ps -aq -f name=sqlserver)" ]; then
    echo "🔄 SQL Server container already exists. Starting..."
    docker start sqlserver
else
    echo "🐳 Creating and starting SQL Server container..."
    docker run -e "ACCEPT_EULA=Y" -e 'SA_PASSWORD=UsingTheF0rce!' \
      -p 1433:1433 --name sqlserver --hostname sqlserver \
      -d mcr.microsoft.com/mssql/server:2022-latest
fi

# Wait for SQL Server to be ready
echo "⏳ Waiting for SQL Server to be ready..."
sleep 10

# Check if EF tools are installed
if ! dotnet tool list -g | grep -q dotnet-ef; then
    echo "📦 Installing Entity Framework Core tools..."
    dotnet tool install --global dotnet-ef
fi

# Apply database migrations
echo "🗄️ Setting up database..."
dotnet ef database update --project StarDestroyer.Domain --startup-project StarDestroyer.Api

echo ""
echo "✅ Setup complete!"
echo ""
echo "Next steps:"
echo "1. Start the API: cd StarDestroyer.Api && dotnet run"
echo "2. Start the React app: cd stardestroyer-app && npm install && npm start"
echo ""
echo "Then visit http://localhost:3000 to use the application!"
echo ""
echo "May the Force be with you! ⭐"
