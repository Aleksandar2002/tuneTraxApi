namespace Domain.Entities;

public class GenreTrack
{
    public int GenreId { get; set; }
    public int TrackId { get; set; }

    public virtual Track Track { get; set; }
    public virtual Genre Genre { get; set; }

}
