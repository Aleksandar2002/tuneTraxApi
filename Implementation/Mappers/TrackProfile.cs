using Application.DTO.Album;
using Application.DTO.Artist;
using Application.DTO.Genre;
using Application.DTO.Track;
using AutoMapper;
using DataAccess;
using Domain.Entities;

namespace Implementation.Mappers;

public class TrackProfile : Profile
{
    public TrackProfile()
    {
        TuneTraxContext _context = new TuneTraxContext();

        CreateMap<Track, DetailedTrackDto>()
        .ForMember(d => d.CoverImage, x => x.MapFrom(s => s.CoverImage != null ? s.CoverImage : "default.jpg"))
        .ForMember(d => d.AudioFile, x => x.MapFrom(s => $"trackAudioFiles/{s.AudioFile}"))
        .ForMember(d => d.Album, x => x.MapFrom((s, d, _, context) => s.Album != null ? context.Mapper.Map<AlbumDto>(s.Album) : null))
        .ForMember(d => d.Artists, x => x.MapFrom((s, d, _, context) => s.Artists.Select(ar => context.Mapper.Map<ArtistDto>(ar)).ToList()))
        .ForMember(d => d.Genres, x => x.MapFrom((s, d, _, context) => s.Genres.Select(g => context.Mapper.Map<GenreDto>(g)).ToList()));


        CreateMap<Track, TrackDto>();

        CreateMap<CreateTrackDto, Track>()
        .ForMember(d => d.AlbumId, y => y.MapFrom(s => s.AlbumId))
        .ForMember(d => d.CoverImage, y => y.MapFrom(s => s.CoverImage))
        .ForMember(d => d.Genres, y => y.MapFrom(s => _context.Genres.Where(x => s.Genres.Contains(x.Id) && x.IsActive).ToList()))
        .ForMember(d => d.Artists, y => y.MapFrom(s => _context.Artists.Where(x => s.Artists.Contains(x.Id) && x.IsActive).ToList()));

    }

}
