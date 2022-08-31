namespace Application.Dto
{
    public class ItemQuery
    {
        public string searchPhrase { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
