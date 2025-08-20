-- Pre-Deployment Script: Environment Setup
-- This script runs before the database schema is deployed

PRINT 'Starting pre-deployment setup...'

-- Ensure the database exists and is in the correct state
IF DB_NAME() = 'StarDestroyerDB'
BEGIN
    PRINT 'Connected to StarDestroyerDB database.'
    
    -- Set database options for optimal EF Core performance
    IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'StarDestroyerDB' AND snapshot_isolation_state = 1)
    BEGIN
        PRINT 'Enabling snapshot isolation...'
        ALTER DATABASE StarDestroyerDB SET ALLOW_SNAPSHOT_ISOLATION ON
    END
    
    IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'StarDestroyerDB' AND is_read_committed_snapshot_on = 1)
    BEGIN
        PRINT 'Enabling read committed snapshot...'
        ALTER DATABASE StarDestroyerDB SET READ_COMMITTED_SNAPSHOT ON
    END
END
ELSE
BEGIN
    PRINT 'Warning: Not connected to StarDestroyerDB database.'
END

PRINT 'Pre-deployment setup completed.'
