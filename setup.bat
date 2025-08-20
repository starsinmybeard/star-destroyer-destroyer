@echo off
echo Star Destroyer Destroyer Setup Script
echo ==================================================

REM Check if Docker is running
docker info >nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ Docker is not running. Please start Docker Desktop and try again.
    pause
    exit /b 1
)

echo ✅ Docker is running

REM Check if SQL Server container exists
docker ps -aq -f name=sqlserver >nul 2>&1
if %errorlevel% equ 0 (
    echo 🔄 SQL Server container already exists. Starting...
    docker start sqlserver
) else (
    echo 🐳 Creating and starting SQL Server container...
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=UsingTheF0rce!" -p 1433:1433 --name sqlserver --hostname sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
)

REM Wait for SQL Server to be ready
echo ⏳ Waiting for SQL Server to be ready...
timeout /t 10 /nobreak >nul

REM Check if EF tools are installed
dotnet tool list -g | findstr dotnet-ef >nul
if %errorlevel% neq 0 (
    echo 📦 Installing Entity Framework Core tools...
    dotnet tool install --global dotnet-ef
)

REM Apply database migrations
echo 🗄️ Setting up database...
dotnet ef database update --project StarDestroyer.Database --startup-project StarDestroyer.Api

echo.
echo ✅ Setup complete!
echo.
echo Next steps:
echo 1. Start the API: cd StarDestroyer.Api ^&^& dotnet run
echo 2. Start the React app: cd stardestroyer-app ^&^& npm install ^&^& npm start
echo.
echo Then visit http://localhost:3000 to use the application!
echo.
echo ⭐ May the Force be with you! ⭐
pause
