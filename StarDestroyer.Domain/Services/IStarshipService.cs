namespace StarDestroyer.Domain;

public interface IStarshipService
{
    Task<IEnumerable<Starship>> GetAllAsync();

    Task<Starship?> GetByIdAsync(int id);

    Task<Starship> CreateAsync(Starship starship);

    Task<Starship> UpdateAsync(Starship starship);

    Task<bool> DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);

    Task<int> DeleteAllAsync();

    Task<PagedResultDto<StarshipDto>> GetFilteredStarshipsAsync(StarshipFilterDto filter);

    Task<IEnumerable<string>> GetDistinctStarshipClassesAsync();

    Task<IEnumerable<string>> GetDistinctManufacturersAsync();
}