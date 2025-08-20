namespace StarDestroyer.Domain;

public class StarshipFilterDto
{
    public string? SearchTerm { get; init; }

    public string? StarshipClass { get; init; }

    public string? Manufacturer { get; init; }

    public int? MinLength { get; init; }

    public int? MaxLength { get; init; }

    public string? SortBy { get; init; } = "Name";

    public string? SortDirection { get; init; } = "asc";

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 50;
}