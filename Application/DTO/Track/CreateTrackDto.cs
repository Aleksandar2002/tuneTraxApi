
namespace Application.DTO.Track
{
    public class CreateTrackDto
    {
        public string Title { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string? CoverImage { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string AudioFile { get; set; } = string.Empty;

        public List<int> Genres { get; set; } = new List<int>();
        public List<int> Artists { get; set; } = new List<int>();

        public int? AlbumId { get; set; }
    }
}
