using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Album;
using Application.DTO.Genre;
using Application.DTO.Track;
using Domain.Entities;

namespace Application.DTO.Artist
{
    public class ArtistDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    public class DetailedArtistDto : ArtistDto
    {
        public string? Image { get; set; }
        public string Country { get; set; }

        public DateOnly DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public List<string> Albums { get; set; } = new List<string>();
        public List<string> Tracks { get; set; } = new List<string>();
        public List<string> Genres { get; set; } = new List<string>();
    }
}
