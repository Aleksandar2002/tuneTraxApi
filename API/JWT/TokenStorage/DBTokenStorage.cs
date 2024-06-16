using System.Security.Claims;
using DataAccess;
using Domain.Entities;

namespace API.JWT.TokenStorage;

public class DBTokenStorage : ITokenStorage
{
    private TuneTraxContext _context;
    public DBTokenStorage(TuneTraxContext context)
    {
        _context = context;

    }


    public void Add(ICollection<Claim> claims)
    {

        var tokenId = Guid.Parse(claims.First(x => x.Type == "jti").Value);
        var userId = int.Parse(claims.First(x => x.Type == "UserId").Value);

        if (!Exists(tokenId))
        {

            _context.InvalidTokens.Add(new InvalidToken
            {
                TokenId = tokenId,
                AddedAt = DateTime.UtcNow,
                UserId = userId
            });
            _context.SaveChanges();
        }

    }


    public void Remove(Guid id)
    {
        throw new NotImplementedException();
    }


    public bool Exists(Guid id)
    {
        if (_context.InvalidTokens.Any(x => x.TokenId == id))
        {
            return true;
        }
        return false;
    }
}
