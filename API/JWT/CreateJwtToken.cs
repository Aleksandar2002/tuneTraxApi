using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using API.DTO;
using DataAccess;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace API.JWT;

public class CreateJwtToken
{
    private TuneTraxContext _context;
    private AppSettings _settings;

    public CreateJwtToken(TuneTraxContext context, AppSettings settings)
    {
        _context = context;
        _settings = settings;
    }


    public string MakeToken(string email, string password)
    {
        var user = _context.Users.Where(x => x.Email == email && x.IsActive).Select(x => new
        {
            x.Username,
            UseCases = x.UseCases.Select(x => x.UseCaseId),
            x.Email,
            x.Id,
            x.Password
        }).FirstOrDefault();

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Jwt.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>{
            new Claim("UserId", Convert.ToString(user.Id), ClaimValueTypes.Integer64),
            new Claim("Email", user.Email),
            new Claim("Username", user.Username),
            new Claim("UseCases", JsonConvert.SerializeObject(user.UseCases)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(JwtRegisteredClaimNames.Iss, _settings.Jwt.Issuer),
        };

        var now = DateTime.UtcNow;

        var token = new JwtSecurityToken(
            issuer: _settings.Jwt.Issuer,
            audience: "Any",
            claims: claims,
            notBefore: now,
            expires: now.AddSeconds(_settings.Jwt.Seconds),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);

    }
}
