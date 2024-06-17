using Application.DTO.Album;
using Application.DTO.Artist;
using Application.DTO.Comment;
using Application.DTO.Genre;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Track
{
    public class TrackDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string AudioFile { get; set; }
        public string? CoverImage { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    public class DetailedTrackDto : TrackDto
    {


        public List<GenreDto> Genres { get; set; }
        public List<ArtistDto> Artists { get; set; }
        // public List<CommentDto> Comments { get; set; }

        public AlbumDto Album { get; set; }
    }
}
