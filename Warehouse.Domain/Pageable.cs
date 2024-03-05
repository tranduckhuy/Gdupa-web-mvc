namespace Warehouse.Domain;

public class Pageable
{
    public int TotalItems { get; private set; }
    public int CurrentPage { get; private set; }
    public int StartPage { get; private set; }
    public int EndPage { get; private set; }
    public int TotalPages { get; private set; }

    public Pageable()
    {
    }

    public Pageable(int totalItems, int page, int pageSize = 10)
    {
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
        CurrentPage = page > TotalPages ? TotalPages : page;
        StartPage = CurrentPage > 3 ? CurrentPage - 3 : 1;
        EndPage = TotalPages - 3 > CurrentPage ? CurrentPage + 3 : TotalPages;
    }
}
