using Application.DTO.Genre;
using AutoMapper;
using Domain.Entities;

namespace Implementation.Mappers;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>();
    }
}
