namespace TreesApi.DataAccess.Models;

public class PageableResult<T>
{
    public int Skip { get; set; }
    public int Count { get; set; }
    public ICollection<T>? Items { get; set; }
}
