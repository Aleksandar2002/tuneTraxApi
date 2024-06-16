using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment : Entity
    {
        public string Text { get; set; } = "";
        public int UserId { get; set; }
        public int TrackId { get; set; }
        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }
        public virtual ICollection<Comment> Children { get; set; }
        public virtual Track Track { get; set; }
        public virtual User User { get; set; }


    }
}
