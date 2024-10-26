namespace KoiShow.MVCWebApp.Helpers
{
    public class PaginationHelper<T>
    {
        public static PagedResult<T> GetPagedData(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalItems = source.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = source
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            return new PagedResult<T>
            {
                TotalItems = totalItems,
                PageSize = pageSize,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                Items = items
            };
        }
    }

    public class PagedResult<T>
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public List<T> Items { get; set; }

    }

}