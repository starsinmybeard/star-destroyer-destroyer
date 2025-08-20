using StarDestroyer.Domain.Models;

namespace StarDestroyer.Domain.Interfaces;

public interface IStarshipRepository
{
    Task<IEnumerable<Starship>> GetAllAsync();

    Task<Starship?> GetByIdAsync(int id);

    Task<Starship> CreateAsync(Starship starship);

    Task<Starship> UpdateAsync(Starship starship);

    Task<bool> DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}

public interface ISwapiService
{
    Task<IEnumerable<Starship>> FetchStarshipsFromSwapiAsync();
}
