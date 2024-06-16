using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Album
{
    public class AlbumDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
    public class DetailedAlbumDto : AlbumDto
    {
        public string Artist { get; set; }
        public List<string> Tracks { get; set; }
    }
}
