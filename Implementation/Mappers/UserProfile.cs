using Application.DTO.User;
using AutoMapper;
using Domain.Entities;

namespace Implementation.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
        .ForMember(d => d.Country, x => x.MapFrom(s => s.Country.Name));
    }
}
