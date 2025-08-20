using Microsoft.AspNetCore.Mvc;
using StarDestroyer.Domain;

namespace StarDestroyer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StarshipsController : ControllerBase
{
    private readonly IStarshipService StarshipService;
    
    private readonly ISwapiService SwApiService;
    
    private readonly ILogger<StarshipsController> Logger;

    public StarshipsController(
        IStarshipService starshipService,
        ISwapiService swapiService,
        ILogger<StarshipsController> logger)
    {
        StarshipService = starshipService;
        SwApiService = swapiService;
        Logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Starship>>> GetStarships()
    {
        try
        {
            var starships = await StarshipService.GetAllAsync();
            return Ok(starships);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error retrieving starships");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("starship-classes")]
    public async Task<ActionResult<IEnumerable<string>>> GetStarshipClasses()
    {
        try
        {
            var classes = await StarshipService.GetDistinctStarshipClassesAsync();
            return Ok(classes);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error retrieving starship classes");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("manufacturers")]
    public async Task<ActionResult<IEnumerable<string>>> GetManufacturers()
    {
        try
        {
            var manufacturers = await StarshipService.GetDistinctManufacturersAsync();
            return Ok(manufacturers);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error retrieving manufacturers");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Starship>> GetStarship(int id)
    {
        try
        {
            var starship = await StarshipService.GetByIdAsync(id);
            if (starship == null)
                return NotFound();

            return Ok(starship);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error retrieving starship with id {Id}", id);
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

            var createdStarship = await StarshipService.CreateAsync(starship);
            return CreatedAtAction(nameof(GetStarship), new { id = createdStarship.Id }, createdStarship);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error creating starship");
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

            if (!await StarshipService.ExistsAsync(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await StarshipService.UpdateAsync(starship);
            return NoContent();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error updating starship with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("all")]
    public async Task<IActionResult> DeleteAllStarships()
    {
        try
        {
            var count = await StarshipService.DeleteAllAsync();
            return Ok($"Successfully deleted {count} starships");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error deleting all starships");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStarship(int id)
    {
        try
        {
            var deleted = await StarshipService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error deleting starship with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("seed")]
    public async Task<IActionResult> SeedFromSwapi()
    {
        try
        {
            var swapiStarships = await SwApiService.FetchStarshipsFromSwapiAsync();
            
            foreach (var starship in swapiStarships)
            {
                await StarshipService.CreateAsync(starship);
            }

            return Ok($"Successfully seeded {swapiStarships.Count()} starships from SWAPI");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error seeding starships from SWAPI");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("filtered")]
    public async Task<ActionResult<PagedResultDto<StarshipDto>>> GetFilteredStarships([FromBody] StarshipFilterDto filter)
    {
        try
        {
            var result = await StarshipService.GetFilteredStarshipsAsync(filter);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error retrieving filtered starships");
            return StatusCode(500, "Internal server error");
        }
    }
}
