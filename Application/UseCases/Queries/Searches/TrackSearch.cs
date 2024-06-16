using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Searches.Track
{
    public class TrackSearch : PagedSearch
    {
        public string? Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Album { get; set; }
        public string? Artist { get; set; }
        public string? Genre { get; set; }

        public string? Ids { get; set; }

    }
}
