-- Post-Deployment Script: Seed Starships from SWAPI
-- This script runs after the database schema is deployed

PRINT 'Starting post-deployment data seeding...'

-- Only seed if the table is empty
IF NOT EXISTS (SELECT 1 FROM [Starships])
BEGIN
    PRINT 'Seeding initial starship data...'
    
    INSERT INTO [Starships] 
    ([Name], [Model], [Manufacturer], [CostInCredits], [Length], [MaxAtmospheringSpeed], 
     [Crew], [Passengers], [CargoCapacity], [Consumables], [HyperdriveRating], 
     [MGLT], [StarshipClass], [Created], [Edited], [Url])
    VALUES 
    ('Death Star', 'DS-1 Orbital Battle Station', 'Imperial Department of Military Research, Sienar Fleet Systems', 
     '1000000000000', '120000', 'n/a', '342,953', '843,342', '1000000000000', '3 years', 
     '4.0', '10', 'Deep Space Mobile Battlestation', GETUTCDATE(), GETUTCDATE(), 
     'https://swapi.dev/api/starships/9/'),
    
    ('Millennium Falcon', 'YT-1300 light freighter', 'Corellian Engineering Corporation', 
     '100000', '34.37', '1050', '4', '6', '100000', '2 months', 
     '0.5', '75', 'Light freighter', GETUTCDATE(), GETUTCDATE(), 
     'https://swapi.dev/api/starships/10/'),
     
    ('Y-wing', 'BTL Y-wing', 'Koensayr Manufacturing', 
     '134999', '14', '1000km', '2', '0', '110', '1 week', 
     '1.0', '80', 'assault starfighter', GETUTCDATE(), GETUTCDATE(), 
     'https://swapi.dev/api/starships/12/')

    PRINT 'Initial starship data seeded successfully.'
END
ELSE
BEGIN
    PRINT 'Starships table already contains data. Skipping seed.'
END

PRINT 'Post-deployment script completed.'
