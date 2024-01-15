namespace WarehouseWebMVC.Models
{
    public class Pageable
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public Pageable()
        {
        }

        public Pageable(int totalItems, int page, int pageSize = 10)
        {
            TotalItems = totalItems;
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            CurrentPage = page;
            StartPage = CurrentPage > 3 ? CurrentPage - 3 : 1;
            EndPage = totalPages - 3 > CurrentPage ? CurrentPage + 3 : totalPages;
        }
    }
}
