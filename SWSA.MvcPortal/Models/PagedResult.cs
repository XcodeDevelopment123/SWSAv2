namespace SWSA.MvcPortal.Models;

public class PagedResult<T> where T : class
{
    public List<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }

    public int TotalPages => (int)System.Math.Ceiling((double)TotalCount / PageSize);
}
