using Application;
using Application.DTO.User;
using Application.UseCases.Queries.Users;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Implementation.UseCases.Queries;

public class EFGetUsersQuery : EFUseCase, IGetUsersQuery
{
    private IMapper _mapper;
    public EFGetUsersQuery(TuneTraxContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;

    }


    public string Name => "Get users";

    public int Id => 2;

    public PagedResponse<UserDto> Execute(UserSearch search)
    {

        var query = Context.Users.AsQueryable();

        var totalCount = query.Count();
        var perPage = search.PerPage.HasValue ? search.PerPage.Value : 10;
        var page = search.Page.HasValue ? search.Page.Value : 10;

        var users = query.Include(x => x.Country).Skip((page - 1) * perPage).Take(perPage);


        return new PagedResponse<UserDto>
        {
            PerPage = perPage,
            Data = users.Select(x => _mapper.Map<UserDto>(x)),
            CurrentPage = page,
            TotalCount = totalCount
        };
    }

}
