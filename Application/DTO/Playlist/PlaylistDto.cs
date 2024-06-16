using Domain.Entities;

namespace Application.DTO.Playlist;

public class PlaylistDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccessLevel AccessLevel { get; set; }
    public IEnumerable<int> Tracks { get; set; } = new List<int>();

}
