namespace Application.UseCases.Queries.Searches;

public class CommentSearch : PagedSearch
{
    public int? TrackId { get; set; }
}
