namespace StarDestroyer.Domain;

public class PagedResultDto<T>
{
    public IEnumerable<T> Items { get; init; } = new List<T>();

    public int TotalCount { get; init; }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;
}