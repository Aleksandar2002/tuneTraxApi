using Application;

namespace API.JWT
{
    public class JwtActor : IApplicationActor
    {
        public int Id{ get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IEnumerable<int> AllowedUseCases{ get; set; }

    }
}
