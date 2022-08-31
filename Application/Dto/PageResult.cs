namespace Application.Dto
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemFrom { get; set; }
        public int ItemTo { get; set; }
        public int TotalItemsCount { get; set; }

        public PageResult(List<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemsCount = totalCount;
            ItemFrom = (pageNumber - 1) * pageSize + 1;
            ItemTo = ItemFrom + pageSize - 1;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
