using Microsoft.EntityFrameworkCore;
using StarDestroyer.Database.Context;
using StarDestroyer.Domain.Interfaces;
using StarDestroyer.Domain.Models;

namespace StarDestroyer.Database.Repositories;

public class StarshipRepository : IStarshipRepository
{
    private readonly StarDestroyerDbContext _context;

    public StarshipRepository(StarDestroyerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Starship>> GetAllAsync()
    {
        return await _context.Starships.ToListAsync();
    }

    public async Task<Starship?> GetByIdAsync(int id)
    {
        return await _context.Starships.FindAsync(id);
    }

    public async Task<Starship> CreateAsync(Starship starship)
    {
        _context.Starships.Add(starship);
        await _context.SaveChangesAsync();
        return starship;
    }

    public async Task<Starship> UpdateAsync(Starship starship)
    {
        _context.Entry(starship).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return starship;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var starship = await _context.Starships.FindAsync(id);
        if (starship == null)
            return false;

        _context.Starships.Remove(starship);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Starships.AnyAsync(s => s.Id == id);
    }
}
