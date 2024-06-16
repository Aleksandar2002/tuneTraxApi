using Application.DTO.User;

namespace Application.UseCases.Queries.Users;

public interface IFindUserQuery : IQuery<int, UserDto>
{

}
