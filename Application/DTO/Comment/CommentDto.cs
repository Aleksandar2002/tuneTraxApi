using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Comment
{
    public class CommentDto
    {
        public string Text { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CommentDto> Children { get; set; }
    }
}
