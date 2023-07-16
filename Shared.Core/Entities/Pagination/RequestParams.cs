namespace Shared.Core.Entities.Pagination;

public abstract class RequestParams
{
    private const int MaxPageSize = 50;

    private int _pageSize;

    public int PageIndex { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}