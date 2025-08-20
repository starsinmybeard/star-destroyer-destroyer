using System.ComponentModel.DataAnnotations;

namespace StarDestroyer.Domain.Models;

public class Starship
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string Model { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string Manufacturer { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string CostInCredits { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string Length { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string MaxAtmospheringSpeed { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string Crew { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string Passengers { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string CargoCapacity { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string Consumables { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string HyperdriveRating { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string MGLT { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string StarshipClass { get; set; } = string.Empty;
    
    public DateTime Created { get; set; }
    
    public DateTime Edited { get; set; }
    
    [MaxLength(500)]
    public string Url { get; set; } = string.Empty;
}
