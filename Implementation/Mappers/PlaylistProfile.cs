using Application.DTO.Playlist;
using AutoMapper;
using DataAccess;
using Domain.Entities;

namespace Implementation.Mappers;

public class PlaylistProfile : Profile
{
    public PlaylistProfile()
    {
        CreateMap<Playlist, PlaylistDto>()
        .ForMember(d => d.Tracks, x => x.MapFrom((s) => s.Tracks.Select(y => y.Id).ToList()));
    }
}
