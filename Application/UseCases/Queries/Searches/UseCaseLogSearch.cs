namespace Application.UseCases.Queries.Searches;

public class UseCaseLogSearch : PagedSearch
{
    public string? Username { get; set; }
    public string? UseCaseName { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

}
