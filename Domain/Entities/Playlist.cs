using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Playlist : NamedEntity
    {
        public AccessLevel AccessLevel { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}
