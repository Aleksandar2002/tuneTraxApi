using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class Actor : IApplicationActor
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Id { get; set; }
        public IEnumerable<int> AllowedUseCases { get; set; } = new List<int>();
    }

    public class UnauthorizedActor : IApplicationActor
    {
        public int Id => 0;

        public string Email => "";

        public string Username => "unauthorized";

        public IEnumerable<int> AllowedUseCases => new List<int> { 1, 2 };
    }
}
