using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab39.Services;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}

public interface IPaginationService<T>
{
    PagedResult<T> Paginate(IEnumerable<T> source, int page, int pageSize);
}

public class PaginationService<T> : IPaginationService<T>
{
    public PagedResult<T> Paginate(IEnumerable<T> source, int page, int pageSize)
    {
        var list = source.ToList();
        return new PagedResult<T>
        {
            Items = list.Skip((page - 1) * pageSize).Take(pageSize),
            TotalCount = list.Count,
            Page = page,
            PageSize = pageSize
        };
    }
}
