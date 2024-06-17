using System.Reflection.Metadata;

namespace API.DTO
{
    public class AppSettings
    {
        public JwtSettings Jwt { get; set; }
        public string ConnectionString { get; set; }
        // public string BugSnugKey { get; set; }
        // public string PasswordSalt { get; set; }
    }
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int Seconds { get; set; }
        public string Issuer { get; set; }
    }
}
