using Application.DTO.Album;
using AutoMapper;
using Domain.Entities;

namespace Implementation.Mappers;

public class AlbumProfile : Profile
{
    public AlbumProfile()
    {
        CreateMap<Album, DetailedAlbumDto>()
        .ForMember(d => d.Artist, y => y.MapFrom(s => s.Artist.Name))
        .ForMember(d => d.Tracks, y => y.MapFrom(s => s.Tracks.Select(x => x.Title).ToList()));

        CreateMap<Album, AlbumDto>();
    }

}
