#!/bin/bash

# Database Deployment Script for Star Destroyer
# Usage: ./deploy-database.sh [environment] [connection-string]
# Example: ./deploy-database.sh development "Server=localhost,1433;Database=StarDestroyerDB;User Id=sa;Password=UsingTheF0rce!;TrustServerCertificate=true;"

set -e  # Exit on error

ENVIRONMENT=${1:-development}
CONNECTION_STRING=${2:-"Server=localhost,1433;Database=StarDestroyerDB;User Id=sa;Password=UsingTheF0rce!;TrustServerCertificate=true;"}

echo "🗄️ Star Destroyer Database Deployment"
echo "=================================="
echo "Environment: $ENVIRONMENT"
echo "Target: $(echo $CONNECTION_STRING | sed 's/Password=[^;]*/Password=***/g')"
echo ""

# Function to run SQL script
run_sql_script() {
    local script_path=$1
    local script_name=$(basename "$script_path")
    
    if [ -f "$script_path" ]; then
        echo "📄 Executing: $script_name"
        sqlcmd -S localhost,1433 -U sa -P "UsingTheF0rce!" -d StarDestroyerDB -i "$script_path" -b
        if [ $? -eq 0 ]; then
            echo "✅ $script_name completed successfully"
        else
            echo "❌ $script_name failed"
            exit 1
        fi
    else
        echo "⚠️  Script not found: $script_path"
    fi
}

# Check if database exists
echo "🔍 Checking database connection..."
sqlcmd -S localhost,1433 -U sa -P "UsingTheF0rce!" -Q "SELECT DB_NAME()" -b > /dev/null
if [ $? -ne 0 ]; then
    echo "❌ Cannot connect to database server"
    exit 1
fi
echo "✅ Database server connection successful"

# Run pre-deployment scripts
echo ""
echo "🔧 Running pre-deployment scripts..."
for script in Scripts/PreDeployment/*.sql; do
    if [ -f "$script" ]; then
        run_sql_script "$script"
    fi
done

# Apply EF Core migrations
echo ""
echo "🏗️  Applying Entity Framework migrations..."
cd ..
dotnet ef database update --project StarDestroyer.Domain --startup-project StarDestroyer.Api
if [ $? -eq 0 ]; then
    echo "✅ EF Core migrations applied successfully"
else
    echo "❌ EF Core migrations failed"
    exit 1
fi
cd StarDestroyer.Database

# Run post-deployment scripts
echo ""
echo "🌱 Running post-deployment scripts..."
for script in Scripts/PostDeployment/*.sql; do
    if [ -f "$script" ]; then
        run_sql_script "$script"
    fi
done

echo ""
echo "🎉 Database deployment completed successfully!"
echo "Environment: $ENVIRONMENT"
echo "May the Force be with your database! ⭐"
