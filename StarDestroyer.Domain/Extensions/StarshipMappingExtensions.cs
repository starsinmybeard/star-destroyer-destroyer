namespace StarDestroyer.Domain;

public static class StarshipMappingExtensions
{
    /// <summary>
    /// Maps a StarshipDto (from SWAPI) to a Starship entity (for database)
    /// </summary>
    public static Starship ToStarship(this StarshipDto dto)
    {
        return new Starship
        {
            Name = dto.Name,
            Model = dto.Model,
            Manufacturer = dto.Manufacturer,
            CostInCredits = dto.CostInCredits,
            Length = dto.Length,
            MaxAtmospheringSpeed = dto.MaxAtmospheringSpeed,
            Crew = dto.Crew,
            Passengers = dto.Passengers,
            CargoCapacity = dto.CargoCapacity,
            Consumables = dto.Consumables,
            HyperdriveRating = dto.HyperdriveRating,
            MGLT = dto.MGLT,
            StarshipClass = dto.StarshipClass,
            Created = DateTime.TryParse(dto.Created, out var created) ? created : DateTime.UtcNow,
            Edited = DateTime.TryParse(dto.Edited, out var edited) ? edited : DateTime.UtcNow,
            Url = dto.Url
        };
    }

    /// <summary>
    /// Maps a Starship entity (from database) to a StarshipDto (for API responses)
    /// </summary>
    public static StarshipDto ToStarshipDto(this Starship starship)
    {
        return new StarshipDto
        {
            Name = starship.Name,
            Model = starship.Model,
            Manufacturer = starship.Manufacturer,
            CostInCredits = starship.CostInCredits,
            Length = starship.Length,
            MaxAtmospheringSpeed = starship.MaxAtmospheringSpeed,
            Crew = starship.Crew,
            Passengers = starship.Passengers,
            CargoCapacity = starship.CargoCapacity,
            Consumables = starship.Consumables,
            HyperdriveRating = starship.HyperdriveRating,
            MGLT = starship.MGLT,
            StarshipClass = starship.StarshipClass,
            Pilots = new List<string>(),
            Films = new List<string>(),
            Created = starship.Created.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            Edited = starship.Edited.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            Url = starship.Url
        };
    }
}
