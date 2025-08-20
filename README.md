# Star Destroyer - Starship Manager

A full-stack web application for managing Star Wars starships using the SWAPI (Star Wars API). Built with .NET 8, Entity Framework Core, React with TypeScript, and Bootstrap.

![Star Wars Starship Manager](https://img.shields.io/badge/Star%20Wars-API-yellow?style=for-the-badge&logo=starwars)
<svg xmlns="http://www.w3.org/2000/svg" width="113.75" height="28" role="img" aria-label=".NET: 8.0"><title>.NET: 8.0</title><g shape-rendering="crispEdges"><rect width="69" height="28" fill="#555"/><rect x="69" width="44.75" height="28" fill="#512bd4"/></g><g fill="#fff" text-anchor="middle" font-family="Verdana,Geneva,DejaVu Sans,sans-serif" text-rendering="geometricPrecision" font-size="100"><image x="9" y="7" width="14" height="14" href="data:image/svg+xml;base64,PHN2ZyBmaWxsPSJ3aGl0ZXNtb2tlIiByb2xlPSJpbWciIHZpZXdCb3g9IjAgMCAyNCAyNCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48dGl0bGU+Lk5FVDwvdGl0bGU+PHBhdGggZD0iTTI0IDguNzdoLTIuNDY4djcuNTY1aC0xLjQyNVY4Ljc3aC0yLjQ2MlY3LjUzSDI0em0tNi44NTIgNy41NjVoLTQuODIxVjcuNTNoNC42M3YxLjI0aC0zLjIwNXYyLjQ5NGgyLjk1M3YxLjIzNGgtMi45NTN2Mi42MDRoMy4zOTZ6bS02LjcwOCAwSDguODgyTDQuNzggOS44NjNhMi44OTYgMi44OTYgMCAwIDEtLjI1OC0uNTFoLS4wMzZjLjAzMi4xODkuMDQ4LjU5Mi4wNDggMS4yMXY1Ljc3MkgzLjE1N1Y3LjUzaDEuNjU5bDMuOTY1IDYuMzJjLjE2Ny4yNjEuMjc1LjQ0Mi4zMjMuNTRoLjAyNGMtLjA0LS4yMzMtLjA2LS42MjktLjA2LTEuMTg1VjcuNTI5aDEuMzcyem0tOC43MDMtLjY5M2EuODY4LjgyOSAwIDAgMS0uODY5LjgyOS44NjguODI5IDAgMCAxLS44NjgtLjgzLjg2OC44MjkgMCAwIDEgLjg2OC0uODI4Ljg2OC44MjkgMCAwIDEgLjg2OS44MjlaIi8+PC9zdmc+"/><text transform="scale(.1)" x="430" y="175" textLength="280" fill="#fff">.NET</text><text transform="scale(.1)" x="913.75" y="175" textLength="207.5" fill="#fff" font-weight="bold">8.0</text></g></svg>
![React](https://img.shields.io/badge/React-18-61DAFB?style=for-the-badge&logo=react)
![TypeScript](https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?style=for-the-badge&logo=bootstrap)

##  Features

- **Full CRUD Operations** for starships
- **Data Seeding** from SWAPI (Star Wars API)
- **Responsive, Sortable Table** with Bootstrap styling
- **EF Core** with SQL Server
- **RESTful API** with Swagger
- **React Frontend** with Typescript
- **CORS Configuration** 

## Tech Stack

### Backend
- **.NET 8**
- **ASP.NET Core**
- **Entity Framework Core 8.0.8**
- **SQL Server** - (via Docker)
- **Swagger/OpenAPI** 

### Frontend
- **React 18** - UI Framework
- **TypeScript** - Type Safety
- **Bootstrap 5** - CSS Framework
- **React Bootstrap** - UI Components
- **Axios** - HTTP Client

## Quick Start Guide
### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 16+](https://nodejs.org/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Git](https://git-scm.com/)

### 1. Clone the Repository
```bash
git clone https://github.com/starsinmybeard/StarDestroyerDestroyer.git
cd StarDestroyerDestroyer
```

### 2. Start the Docker server
```bash
docker run -e "ACCEPT_EULA=Y" -e 'SA_PASSWORD=UsingTheF0rce!' \
  -p 1433:1433 --name sqlserver --hostname sqlserver \
  -d mcr.microsoft.com/mssql/server:2022-latest

# For Windows PowerShell
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=UsingTheF0rce!" -p 1433:1433 --name sqlserver --hostname sqlserver -d mcr.microsoft.com/mssql/server:2022-latest

# Verify container is running
docker ps
```

### 3. Setup Database
```bash
# Install EF Core tools (if not already installed)
dotnet tool install --global dotnet-ef

# Apply database migrations
dotnet ef database update --project StarDestroyer.Database --startup-project StarDestroyer.Api
```

### 4. Start the Backend API
```bash
cd StarDestroyer.Api
dotnet run
```
The API will be available at `http://localhost:5069`, but the page will be blank.
- Access the data via the Swagger UI
- `http://localhost:5069/swagger/index.html`

### 5. Start the Frontend in a new terminal
```bash
cd stardestroyer-app
npm install
npm start
```
The React app will be available at `http://localhost:3000`

## Using the App

1. **Open the app** Open `http://localhost:3000` in your browser
2. **Seed Data**: Click "Pull data from the Star Wars API" to fill the database with starships
3. **View Starships**: Browse all starships in the table
4. **Add Starship**: Click "Add Starship" to create a new entry
5. **Edit Starship**: Click "Edit" on any row to modify a starship
6. **Delete Starship**: Click "Delete" to remove a starship

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/starships` | Get all starships |
| GET | `/api/starships/{id}` | Get starship by ID |
| POST | `/api/starships` | Create new starship |
| PUT | `/api/starships/{id}` | Update starship |
| DELETE | `/api/starships/{id}` | Delete starship |
| POST | `/api/starships/seed` | Seed data from SWAPI |

## Docker Alternative (M1 Mac Users)

If you encounter issues with the SQL Server image on Apple Silicon:

```bash
# Use Azure SQL Edge (ARM64 compatible)
docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=UsingTheF0rce!' -p 1433:1433 --name azuresqledge -d mcr.microsoft.com/azure-sql-edge:latest
```

## Troubleshooting

### Common Issues

**Database Connection Issues:**
```bash
# Check if Docker container is running
docker ps

# Restart container if needed
docker restart sqlserver

# Check if port 1433 is available
lsof -i :1433  # macOS/Linux
netstat -an | find "1433"  # Windows
```

**React App CORS Errors:**
- Ensure API is running on port 5069
- Verify CORS is configured in `Program.cs`

**Build Errors:**
```bash
# Clean and restore .NET projects
dotnet clean
dotnet restore
dotnet build

# Clear React cache
cd stardestroyer-app
rm -rf node_modules package-lock.json
npm install
```

## Project Structure

```
StarDestroyerDestroyer/
├── StarDestroyer.Api/         # ASP.NET Core Web API
├── StarDestroyer.Domain/      # Domain models and interfaces
├── StarDestroyer.Database/    # Entity Framework Core data access
├── stardestroyer-app/         # React TypeScript frontend
├── StarDestroyer.sln          # Solution file
└── README.md                  # This file
```

## Future Enhancements

Potential improvements for extra credit:
- [ ] Docker containerization (full app)
- [ ] Authentication and authorization  
- [ ] Unit and integration tests
- [ ] Cloud deployment
- [ ] Pagination for large datasets
- [ ] AI features integration

## Want to Contribute?

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is for educational purposes as part of a coding exercise.

### Acknowledgments

- [SWAPI (Star Wars API)](https://swapi.dev/) for providing the Star Wars data

---

**May the Force be with you!** 🚀🪐🌕🌑☄️🛸
