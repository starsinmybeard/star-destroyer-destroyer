using System.Text.Json.Serialization;

namespace StarDestroyer.Domain;

public class StarshipDto
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("model")]
    public string Model { get; init; } = string.Empty;

    [JsonPropertyName("manufacturer")]
    public string Manufacturer { get; init; } = string.Empty;

    [JsonPropertyName("cost_in_credits")]
    public string CostInCredits { get; init; } = string.Empty;

    [JsonPropertyName("length")]
    public string Length { get; init; } = string.Empty;

    [JsonPropertyName("max_atmosphering_speed")]
    public string MaxAtmospheringSpeed { get; init; } = string.Empty;

    [JsonPropertyName("crew")]
    public string Crew { get; init; } = string.Empty;

    [JsonPropertyName("passengers")]
    public string Passengers { get; init; } = string.Empty;

    [JsonPropertyName("cargo_capacity")]
    public string CargoCapacity { get; init; } = string.Empty;

    [JsonPropertyName("consumables")]
    public string Consumables { get; init; } = string.Empty;

    [JsonPropertyName("hyperdrive_rating")]
    public string HyperdriveRating { get; init; } = string.Empty;

    [JsonPropertyName("MGLT")]
    public string MGLT { get; init; } = string.Empty;

    [JsonPropertyName("starship_class")]
    public string StarshipClass { get; init; } = string.Empty;

    [JsonPropertyName("pilots")]
    public List<string> Pilots { get; init; } = new ();

    [JsonPropertyName("films")]
    public List<string> Films { get; init; } = new ();

    [JsonPropertyName("created")]
    public string Created { get; init; } = string.Empty;

    [JsonPropertyName("edited")]
    public string Edited { get; init; } = string.Empty;
    
    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;
}