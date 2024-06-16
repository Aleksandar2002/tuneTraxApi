namespace Application.DTO.Album;

public class CreateAlbumDto
{
    public int ArtistId { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
}
