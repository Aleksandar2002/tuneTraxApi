using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Artist : NamedEntity
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateOnly DateOfBirth { get; set; }
        public string? Image { get; set; }
        public int CountryId { get; set; }
        public Gender Gender { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
        public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();


    }
}
