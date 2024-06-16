namespace Domain.Entities;

public class ArtistGenre
{
    public int ArtistId { get; set; }
    public int GenreId { get; set; }

    public virtual Genre Genre { get; set; }
    public virtual Artist Artist { get; set; }
}
