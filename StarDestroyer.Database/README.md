# StarDestroyer Database Project

This project contains database deployment scripts, publish profiles, and database-specific configurations for the Star Destroyer application.

## Structure

```
StarDestroyer.Database/
├── PublishProfiles/           # Database publish profiles for different environments
├── Scripts/
│   ├── PreDeployment/         # Scripts that run before schema deployment
│   └── PostDeployment/        # Scripts that run after schema deployment
├── deploy-database.sh         # Automated deployment script
└── README.md                  # This file
```

## Usage

### Quick Deployment
```bash
# Deploy to development environment
./deploy-database.sh development

# Deploy to production (with custom connection string)
./deploy-database.sh production "Server=prod-server;Database=StarDestroyerDB_Prod;..."
```

### Manual Steps
1. **Pre-deployment**: Run scripts in `Scripts/PreDeployment/` first
2. **Schema deployment**: Apply EF Core migrations from Domain project
3. **Post-deployment**: Run scripts in `Scripts/PostDeployment/` last

### Environment-Specific Deployments

#### Development
- Uses local Docker SQL Server
- Connection: `localhost,1433`
- Database: `StarDestroyerDB`
- Includes sample data seeding

#### Production
- Uses Azure SQL Database or dedicated SQL Server
- Requires secure connection strings
- Additional backup and safety checks
- No sample data seeding

## Script Descriptions

### Pre-Deployment Scripts
- `001_DatabaseSetup.sql` - Configure database settings for optimal EF Core performance

### Post-Deployment Scripts
- `001_SeedStarships.sql` - Seeds initial starship data for development/testing

## Adding New Scripts

1. **Pre-deployment scripts**: Add to `Scripts/PreDeployment/` with naming convention `###_Description.sql`
2. **Post-deployment scripts**: Add to `Scripts/PostDeployment/` with naming convention `###_Description.sql`
3. Scripts are executed in alphabetical order

## CI/CD Integration

The `deploy-database.sh` script is designed to work with CI/CD pipelines:

```yaml
# Example Azure DevOps pipeline step
- script: |
    cd StarDestroyer.Database
    ./deploy-database.sh $(Environment) "$(ConnectionString)"
  displayName: 'Deploy Database'
```

## Best Practices

1. **Always backup** production databases before deployment
2. **Test scripts** in development environment first
3. **Use transactions** in scripts when possible
4. **Include rollback scripts** for complex changes
5. **Version control** all database changes
6. **Document** any manual steps required

## Troubleshooting

### Connection Issues
- Ensure SQL Server is running
- Verify connection string format
- Check firewall settings for remote databases

### Migration Issues
- Ensure EF Core tools are installed: `dotnet tool install --global dotnet-ef`
- Verify startup project is correctly specified
- Check that DbContext is in the Domain project

### Script Execution Issues
- Verify script syntax with SQL Server Management Studio
- Check that sqlcmd is installed and in PATH
- Ensure proper permissions on target database
