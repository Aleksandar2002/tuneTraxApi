using Application.DTO.Album;
using Application.DTO.Artist;
using Application.DTO.Genre;
using Application.DTO.Track;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Implementation.Validators.Artist;

namespace Implementation.Mappers;

public class ArtistProfile : Profile
{
    public ArtistProfile()
    {
        var context = new TuneTraxContext();

        CreateMap<Artist, ArtistDto>();

        CreateMap<Artist, DetailedArtistDto>()
        .ForMember(d => d.Country, x => x.MapFrom(d => d.Country.Name))
        .ForMember(d => d.Image, x => x.MapFrom(d => d.Image != null ? d.Image : "default"))
        .ForMember(d => d.Albums, x => x.MapFrom((s, d, _, context) => s.Albums.Select(a => a.Name).ToList()))
        .ForMember(d => d.Genres, x => x.MapFrom((s, d, _, context) => s.Genres.Select(t => t.Name).ToList()))
        .ForMember(d => d.Tracks, x => x.MapFrom((s, d, _, context) => s.Tracks.Select(t => t.Title).ToList()));

        CreateMap<Artist, CreateArtistDto>().ReverseMap()
        .ForMember(d => d.Genres, x => x.MapFrom(s => context.Genres.Where(g => s.Genres.Contains(g.Id) && g.IsActive).ToList()));
    }
}
