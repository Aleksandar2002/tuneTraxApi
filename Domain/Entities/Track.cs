using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Track : Entity
    {
        public string Title { get; set; } = "";
        public int Duration { get; set; }
        public string? CoverImage { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string AudioFile { get; set; } = "";
        public int? AlbumId { get; set; }

        public virtual Album Album { get; set; }
        public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();
        public virtual ICollection<Comment> Comments{ get; set; } = new List<Comment>();
        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
