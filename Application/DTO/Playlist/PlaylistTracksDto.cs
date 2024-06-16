using System.Diagnostics.Contracts;

namespace Application.DTO.Playlist;

public class PlaylistTracksDto
{
    public ICollection<int> Tracks { get; set; }
    public int? PlaylistId { get; set; }
}
