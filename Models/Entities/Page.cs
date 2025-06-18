namespace TaskManager.Models.Entities;

public abstract class Page<T>
{
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 0;
    public int TotalItems { get; set; } = 0;
    public int TotalPages { get; set; } = 0;
    public List<T> Data { get; set; }
    public Page(List<T> data, int page, int size, int totalItems, int totalPage)
    {
        Data = data;
        PageNumber = page;
        PageSize = size;
        TotalItems = totalItems;
        TotalPages = totalPage;
    }
}