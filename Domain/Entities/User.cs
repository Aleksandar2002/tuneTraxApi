using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : Entity
    {
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public DateOnly DateOfBirth { get; set; }
        public int CountryId { get; set; }
        public Gender Gender { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Playlist> Playlists{ get; set; } = new List<Playlist>();
        public virtual ICollection<UserUseCase> UseCases { get; set; } = new List<UserUseCase>();

        //public int RoleId { get; set; }
        //public virtual Role Role { get; set; }
    }
}
