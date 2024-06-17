namespace Application;

public class PagedResponse<TDto> where TDto : class
{
    public IEnumerable<TDto> Data { get; set; } = new List<TDto>();
    public int PerPage { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    //public int Pages
    //{
    //    get
    //    {
    //        return (int)Math.Ceiling((double)TotalCount / Pages);
    //    }
    //}
}
