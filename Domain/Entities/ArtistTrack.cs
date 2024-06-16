namespace Domain.Entities;

public class ArtistTrack
{
    public int ArtistId { get; set; }
    public int TrackId { get; set; }

    public virtual Artist Artist { get; set; }
    public virtual Track Track { get; set; }
}
