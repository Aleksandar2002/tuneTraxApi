using Application.DTO.Album;
using Application.DTO.Artist;
using Application.DTO.Comment;
using Application.DTO.Genre;
using Application.DTO.Track;
using Application.Exceptions;
using Application.UseCases.Queries;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Implementation.UseCases.Queries
{
    public class EFFindTrackQuery : EFUseCase, IFindTrackQuery
    {
        private IMapper _mapper;
        public EFFindTrackQuery(TuneTraxContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public string Name => "Find track";

        public int Id => 5;

        public DetailedTrackDto Execute(int search)
        {
            var track = Context.Tracks.Where(x => x.Id == search && x.IsActive).Include(x => x.Album).Include(x => x.Artists).Include(x => x.Genres).Select(x => _mapper.Map<DetailedTrackDto>(x)).FirstOrDefault();

            if (track == null)
            {
                throw new EntityNotFoundException(search, typeof(Track).Name);
            }
            return track;
        }
    }
}
