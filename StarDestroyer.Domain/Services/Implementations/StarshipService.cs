using Microsoft.EntityFrameworkCore;

namespace StarDestroyer.Domain;

public class StarshipService(StarDestroyerDbContext dbContext) : IStarshipService
{
    public async Task<IEnumerable<Starship>> GetAllAsync()
    {
        return await dbContext.Starships.ToListAsync();
    }

    public async Task<Starship?> GetByIdAsync(int id)
    {
        return await dbContext.Starships.FindAsync(id);
    }

    public async Task<Starship> CreateAsync(Starship starship)
    {
        dbContext.Starships.Add(starship);
        await dbContext.SaveChangesAsync();
        return starship;
    }

    public async Task<Starship> UpdateAsync(Starship starship)
    {
        dbContext.Entry(starship).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        return starship;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var starship = await dbContext.Starships.FindAsync(id);
        if (starship == null)
            return false;

        dbContext.Starships.Remove(starship);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await dbContext.Starships.AnyAsync(s => s.Id == id);
    }

    public async Task<int> DeleteAllAsync()
    {
        var count = await dbContext.Starships.CountAsync();
        dbContext.Starships.RemoveRange(dbContext.Starships);
        await dbContext.SaveChangesAsync();
        return count;
    }

    public async Task<PagedResultDto<StarshipDto>> GetFilteredStarshipsAsync(StarshipFilterDto filter)
    {
        var query = dbContext.Starships.AsQueryable();

        // Apply text search filter
        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var searchTerm = filter.SearchTerm.ToLower();
            query = query.Where(s => 
                s.Name.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                s.Model.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                s.Manufacturer.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase));
        }

        // Apply starship class filter
        if (!string.IsNullOrWhiteSpace(filter.StarshipClass))
        {
            query = query.Where(s => s.StarshipClass == filter.StarshipClass);
        }

        // Apply manufacturer filter
        if (!string.IsNullOrWhiteSpace(filter.Manufacturer))
        {
            query = query.Where(s => s.Manufacturer == filter.Manufacturer);
        }

        // Get all matching starships for complex filtering and sorting
        var starships = await query.ToListAsync();

        // Apply length filters (client-side for complex parsing)
        if (filter.MinLength.HasValue)
        {
            starships = starships.Where(s => 
                !string.IsNullOrEmpty(s.Length) && 
                int.TryParse(s.Length.Replace(",", ""), out int length) && 
                length >= filter.MinLength.Value).ToList();
        }

        if (filter.MaxLength.HasValue)
        {
            starships = starships.Where(s => 
                !string.IsNullOrEmpty(s.Length) && 
                int.TryParse(s.Length.Replace(",", ""), out int length) && 
                length <= filter.MaxLength.Value).ToList();
        }

        starships = SortStarships(starships, filter.SortBy, filter.SortDirection);

        var totalCount = starships.Count;

        var pagedStarships = starships
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

        var starshipDtos = pagedStarships.Select(s => s.ToStarshipDto()).ToList();

        return new PagedResultDto<StarshipDto>
        {
            Items = starshipDtos,
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<IEnumerable<string>> GetDistinctStarshipClassesAsync()
    {
        return await dbContext.Starships
            .Where(s => !string.IsNullOrEmpty(s.StarshipClass))
            .Select(s => s.StarshipClass)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetDistinctManufacturersAsync()
    {
        return await dbContext.Starships
            .Where(s => !string.IsNullOrEmpty(s.Manufacturer))
            .Select(s => s.Manufacturer)
            .Distinct()
            .OrderBy(m => m)
            .ToListAsync();
    }

    private List<Starship> SortStarships(List<Starship> starships, string? sortBy, string? sortDirection)
    {
        if (string.IsNullOrWhiteSpace(sortBy))
            sortBy = "Name";

        var isDescending = sortDirection?.ToLower() == "desc";

        return sortBy.ToLower() switch
        {
            "name" => isDescending ? 
                starships.OrderByDescending(s => s.Name).ToList() : 
                starships.OrderBy(s => s.Name).ToList(),
            "model" => isDescending ? 
                starships.OrderByDescending(s => s.Model).ToList() : 
                starships.OrderBy(s => s.Model).ToList(),
            "manufacturer" => isDescending ? 
                starships.OrderByDescending(s => s.Manufacturer).ToList() : 
                starships.OrderBy(s => s.Manufacturer).ToList(),
            "starshipclass" => isDescending ? 
                starships.OrderByDescending(s => s.StarshipClass).ToList() : 
                starships.OrderBy(s => s.StarshipClass).ToList(),
            "length" => isDescending ? 
                starships.OrderByDescending(s => GetNumericLength(s.Length)).ToList() :
                starships.OrderBy(s => GetNumericLength(s.Length)).ToList(),
            "crew" => isDescending ? 
                starships.OrderByDescending(s => s.Crew).ToList() : 
                starships.OrderBy(s => s.Crew).ToList(),
            _ => isDescending ? 
                starships.OrderByDescending(s => s.Name).ToList() : 
                starships.OrderBy(s => s.Name).ToList()
        };
    }

    private static int GetNumericLength(string? length)
    {
        if (string.IsNullOrEmpty(length))
            return 0;
            
        if (int.TryParse(length.Replace(",", ""), out int result))
            return result;
            
        return 0;
    }
}