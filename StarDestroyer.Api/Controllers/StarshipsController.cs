using Microsoft.AspNetCore.Mvc;
using StarDestroyer.Domain.Interfaces;
using StarDestroyer.Domain.Models;

namespace StarDestroyer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StarshipsController : ControllerBase
{
    private readonly IStarshipRepository _repository;
    private readonly ISwapiService _swapiService;
    private readonly ILogger<StarshipsController> _logger;

    public StarshipsController(
        IStarshipRepository repository,
        ISwapiService swapiService,
        ILogger<StarshipsController> logger)
    {
        _repository = repository;
        _swapiService = swapiService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Starship>>> GetStarships()
    {
        try
        {
            var starships = await _repository.GetAllAsync();
            return Ok(starships);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving starships");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Starship>> GetStarship(int id)
    {
        try
        {
            var starship = await _repository.GetByIdAsync(id);
            if (starship == null)
                return NotFound();

            return Ok(starship);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving starship with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Starship>> CreateStarship(Starship starship)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdStarship = await _repository.CreateAsync(starship);
            return CreatedAtAction(nameof(GetStarship), new { id = createdStarship.Id }, createdStarship);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating starship");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStarship(int id, Starship starship)
    {
        try
        {
            if (id != starship.Id)
                return BadRequest("ID mismatch");

            if (!await _repository.ExistsAsync(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.UpdateAsync(starship);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating starship with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStarship(int id)
    {
        try
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting starship with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("seed")]
    public async Task<IActionResult> SeedFromSwapi()
    {
        try
        {
            var swapiStarships = await _swapiService.FetchStarshipsFromSwapiAsync();
            
            foreach (var starship in swapiStarships)
            {
                await _repository.CreateAsync(starship);
            }

            return Ok($"Successfully seeded {swapiStarships.Count()} starships from SWAPI");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error seeding starships from SWAPI");
            return StatusCode(500, "Internal server error");
        }
    }
}
