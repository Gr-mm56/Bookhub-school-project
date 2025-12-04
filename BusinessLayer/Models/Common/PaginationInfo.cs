namespace BusinessLayer.Models.Common;

public class PaginationInfo
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; private set; }
    public int TotalCount { get; set; }

    public int TotalPages => TotalCount == 0 ? 1 : (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;
    public int StartIndex => (PageNumber - 1) * PageSize + 1;
    public int EndIndex => Math.Min(PageNumber * PageSize, TotalCount);

    public PaginationInfo(int pageSize = 10)
    {
        PageSize = pageSize;
    }
}

