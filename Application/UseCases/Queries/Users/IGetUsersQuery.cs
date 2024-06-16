using Application.DTO.User;

namespace Application.UseCases.Queries.Users;

public interface IGetUsersQuery : IQuery<UserSearch, PagedResponse<UserDto>>
{

}
