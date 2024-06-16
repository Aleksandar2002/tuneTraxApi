using Application.DTO.User;
using Application.UseCases.Queries.Users;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Implementation.UseCases.Generics;

namespace Implementation.UseCases.Queries;

public class EFFindUserQuery : GenericFind<User, UserDto>, IFindUserQuery
{
    public EFFindUserQuery(TuneTraxContext context, IMapper mapper) : base(context, mapper)
    {
    }


    public override string Name => "Find user";

    public override int Id => 37;

}
