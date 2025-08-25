using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace StarDestroyer.Domain;

public class SwapiService : ISwapiService
{
    private readonly HttpClient HttpClient;

    private readonly ILogger<SwapiService> Logger;

    private const string SwapiBaseUrl = "https://swapi.info/api/";

    public SwapiService(HttpClient httpClient, ILogger<SwapiService> logger)
    {
        HttpClient = httpClient;
        Logger = logger;
    }

    public async Task<IEnumerable<Starship>> FetchStarshipsFromSwapiAsync()
    {
        var starships = new List<Starship>();

        try
        {
            var response = await HttpClient.GetAsync($"{SwapiBaseUrl}starships/");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
            
            var starshipDtos = JsonSerializer.Deserialize<List<StarshipDto>>(jsonString, options);

            if (starshipDtos != null)
            {
                foreach (var starshipDto in starshipDtos)
                {
                    starships.Add(starshipDto.ToStarship());
                }
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching starships from SWAPI");
            throw;
        }

        return starships;
    }
}
