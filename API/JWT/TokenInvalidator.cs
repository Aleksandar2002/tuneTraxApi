using System.IdentityModel.Tokens.Jwt;
using DataAccess;
using Domain.Entities;
using Implementation;

namespace API.JWT;

public class TokenInvalidator
{
    private string _authHeader;
    private TuneTraxContext _context;
    public TokenInvalidator(string authHeader, TuneTraxContext context)
    {
        _authHeader = authHeader;
        _context = context;
    }

    // public void Invalidate()
    // {
    //     var data = _authHeader.Split("Bearer ");
    //     if (data.Length < 2)
    //     {
    //         throw new InvalidOperationException("You need to send token");
    //     }

    //     var token = data[1];

    //     if (_context.InvalidTokens.Any(x => x.Token == token))
    //     {
    //         throw new InvalidOperationException("Invalid token");
    //     }

    //     var tokenJsonObject = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

    //     var claims = tokenJsonObject?.Claims;

    //     if (claims == null)
    //     {
    //         throw new InvalidOperationException("Wrong token format");
    //     }


    //     var tokenId = Guid.Parse(claims.First(x => x.Type == "jti").Value);
    //     var userId = int.Parse(claims.First(x => x.Type == "UserId").Value);

    //     _context.InvalidTokens.Add(new InvalidToken
    //     {
    //         TokenId = tokenId,
    //         Token = token,
    //         AddedAt = DateTime.UtcNow,
    //         UserId = userId
    //     });
    //     _context.SaveChanges();
    // }

}
