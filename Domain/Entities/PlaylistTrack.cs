namespace Domain.Entities;

public class PlaylistTrack
{
    public int PlaylistId { get; set; }
    public int TrackId { get; set; }

    public virtual Track Track { get; set; }
    public virtual Playlist Playlist { get; set; }
}
