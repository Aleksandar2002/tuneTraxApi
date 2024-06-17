using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Genre : NamedEntity
    {
        public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();
        public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}
