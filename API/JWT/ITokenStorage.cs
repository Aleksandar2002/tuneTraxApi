using System.Security.Claims;

namespace API.JWT
{
    public interface ITokenStorage
    {
        void Add(ICollection<Claim> claims);
        bool Exists(Guid id);
        void Remove(Guid id);
    }
}
