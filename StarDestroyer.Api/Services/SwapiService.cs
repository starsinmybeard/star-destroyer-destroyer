using StarDestroyer.Domain.DTOs;
using StarDestroyer.Domain.Interfaces;
using StarDestroyer.Domain.Models;
using System.Text.Json;

namespace StarDestroyer.Api.Services;

public class SwapiService : ISwapiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SwapiService> _logger;
    private const string SwapiBaseUrl = "https://swapi.dev/api/";

    public SwapiService(HttpClient httpClient, ILogger<SwapiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<Starship>> FetchStarshipsFromSwapiAsync()
    {
        var starships = new List<Starship>();
        string? nextUrl = $"{SwapiBaseUrl}starships/";

        try
        {
            while (!string.IsNullOrEmpty(nextUrl))
            {
                var response = await _httpClient.GetAsync(nextUrl);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var swapiResponse = JsonSerializer.Deserialize<SwapiStarshipResponse>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (swapiResponse?.Results != null)
                {
                    foreach (var swapiStarship in swapiResponse.Results)
                    {
                        starships.Add(MapSwapiStarshipToStarship(swapiStarship));
                    }
                }

                nextUrl = swapiResponse?.Next;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching starships from SWAPI");
            throw;
        }

        return starships;
    }

    private static Starship MapSwapiStarshipToStarship(SwapiStarship swapiStarship)
    {
        return new Starship
        {
            Name = swapiStarship.Name,
            Model = swapiStarship.Model,
            Manufacturer = swapiStarship.Manufacturer,
            CostInCredits = swapiStarship.Cost_in_credits,
            Length = swapiStarship.Length,
            MaxAtmospheringSpeed = swapiStarship.Max_atmosphering_speed,
            Crew = swapiStarship.Crew,
            Passengers = swapiStarship.Passengers,
            CargoCapacity = swapiStarship.Cargo_capacity,
            Consumables = swapiStarship.Consumables,
            HyperdriveRating = swapiStarship.Hyperdrive_rating,
            MGLT = swapiStarship.MGLT,
            StarshipClass = swapiStarship.Starship_class,
            Created = DateTime.TryParse(swapiStarship.Created, out var created) ? created : DateTime.UtcNow,
            Edited = DateTime.TryParse(swapiStarship.Edited, out var edited) ? edited : DateTime.UtcNow,
            Url = swapiStarship.Url
        };
    }
}
