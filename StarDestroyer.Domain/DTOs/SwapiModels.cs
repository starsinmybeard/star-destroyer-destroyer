namespace StarDestroyer.Domain.DTOs;

public class SwapiStarshipResponse
{
    public int Count { get; set; }

    public string? Next { get; set; }

    public string? Previous { get; set; }

    public List<SwapiStarship> Results { get; set; } = [];
}

public class SwapiStarship
{
    public string Name { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    public string Cost_in_credits { get; set; } = string.Empty;

    public string Length { get; set; } = string.Empty;

    public string Max_atmosphering_speed { get; set; } = string.Empty;

    public string Crew { get; set; } = string.Empty;

    public string Passengers { get; set; } = string.Empty;

    public string Cargo_capacity { get; set; } = string.Empty;

    public string Consumables { get; set; } = string.Empty;

    public string Hyperdrive_rating { get; set; } = string.Empty;

    public string MGLT { get; set; } = string.Empty;

    public string Starship_class { get; set; } = string.Empty;

    public List<string> Pilots { get; set; } = new ();

    public List<string> Films { get; set; } = new ();

    public string Created { get; set; } = string.Empty;

    public string Edited { get; set; } = string.Empty;
    
    public string Url { get; set; } = string.Empty;
}