using Customers.Shared;

namespace Customers.Requests;

public record SearchRequest
{
    public string? SortBy { get;  }
    public  string? SortOrder { get;  }
    public int PageSize { get;  }
    public int PageNumber { get;  }
    
    protected SearchRequest(string? sortBy, string? sortOrder, int pageSize, int pageNumber)
    {
        SortBy = sortBy;
        SortOrder = sortOrder.IsNullOrEmpty() ? Constants.Ascending : sortOrder;
        PageNumber = pageNumber <= 0 ? 1 : pageNumber;

        PageSize = pageSize switch
        {
            >20 => 20,
            <=0 => 10,
            _ => pageSize
        };
    }
}