using System.IdentityModel.Tokens.Jwt;
using API.Extensions;
using Application;
using DataAccess;
using Implementation;
using Newtonsoft.Json;

namespace API.JWT;

public class JwtActorProvider : IApplicationActorProvider
{
    private HttpRequest _request;
    private TuneTraxContext _context;

    public JwtActorProvider(HttpRequest request, TuneTraxContext context)
    {
        _request = request;
        _context = context;

    }


    public IApplicationActor GetActor()
    {

        if (_request.Headers["Authorization"].ToString().Split("Bearer ").Length != 2)
        {
            return new UnauthorizedActor();
        }

        var claims = _request.GetTokenClaims();

        var exp = long.Parse(claims.First(x => x.Type == "exp").Value);

        var currentUtcTimeInSeconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000;

        if (exp < currentUtcTimeInSeconds)
        {
            throw new InvalidOperationException("Token has expired");
        }

        var email = claims.First(x => x.Type == "Email").Value;
        var id = int.Parse(claims.First(x => x.Type == "UserId").Value);
        var username = claims.First(x => x.Type == "Username").Value;
        var useCases = claims.First(x => x.Type == "UseCases").Value;
        List<int> useCaseIds = JsonConvert.DeserializeObject<List<int>>(useCases);


        return new JwtActor
        {
            Username = username,
            Email = email,
            Id = id,
            AllowedUseCases = useCaseIds,
        };
    }

}
