namespace skit.Shared.Models;

public record PaginationRequest
{
    private int _pageNumber;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value >= 1 ? value : 1;
    }

    private int _pageSize;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value >= 1 ? value <= 100 ? value : 25 : 25;
    }
    
    public string? OrderBy { get; set; }
    public bool IsDesc { get; set; } = false;

    public PaginationRequest()
    {
        _pageNumber = 1;
        _pageSize = 10;
        OrderBy = null;
    }
}