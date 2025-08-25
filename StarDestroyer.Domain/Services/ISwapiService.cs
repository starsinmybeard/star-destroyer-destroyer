namespace StarDestroyer.Domain;

public interface ISwapiService
{
    Task<IEnumerable<Starship>> FetchStarshipsFromSwapiAsync();
}
