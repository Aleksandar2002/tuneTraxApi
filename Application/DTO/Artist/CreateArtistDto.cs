using Domain.Entities;

namespace Application.DTO.Artist
{
    public class CreateArtistDto
    {
        public string? Image { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public List<int> Genres { get; set; } = new List<int>();

    }
}
