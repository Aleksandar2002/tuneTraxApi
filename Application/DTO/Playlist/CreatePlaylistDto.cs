using Domain.Entities;

namespace Application.DTO.Playlist;

public class CreatePlaylistDto
{
    public string Name { get; set; } = string.Empty;
    public AccessLevel AccessLevel { get; set; }
}
