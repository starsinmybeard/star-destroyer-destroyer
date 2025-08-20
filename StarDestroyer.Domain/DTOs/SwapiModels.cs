namespace StarDestroyer.Domain;

public class SwapiStarshipResponse
{
    public int Count { get; init; }
    
    public string? Next { get; init; }

    public string? Previous { get; init; }
    
    public List<StarshipDto> Results { get; init; } = [];
}