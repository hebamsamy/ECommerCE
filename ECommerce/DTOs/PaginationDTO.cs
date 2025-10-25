namespace ECommerce.DTOs
{
    public class PaginationDTO<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<T> List { get; set; }

    }
}
