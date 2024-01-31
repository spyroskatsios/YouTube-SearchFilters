namespace Customers.Results;

public class PaginatedList<T> : List<T>
{
    public MetaData MetaData { get; set; }

    public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        var totalPages = (int)Math.Ceiling(count/ (double) pageSize);
        MetaData = new MetaData(pageNumber, pageSize, count, totalPages, pageNumber > 1,
            pageNumber < totalPages);
        AddRange(items);
    }
}

public record MetaData(int CurrentPage, int PageSize, int TotalCount, int TotalPages, bool HasPrevious, bool HasNext);