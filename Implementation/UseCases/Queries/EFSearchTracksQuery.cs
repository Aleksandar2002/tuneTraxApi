using Application;
using Application.DTO.Album;
using Application.DTO.Artist;
using Application.DTO.Comment;
using Application.DTO.Genre;
using Application.DTO.Track;
using Application.UseCases.Queries;
using Application.UseCases.Queries.Searches.Track;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EFSearchTracksQuery : EFUseCase, ISearchTracksQuery
    {
        private IMapper _mapper;
        public EFSearchTracksQuery(TuneTraxContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public string Name => "Saerch tracks";

        public int Id => 4;

        public PagedResponse<DetailedTrackDto> Execute(TrackSearch search)
        {
            var query = Context.Tracks.Where(x => x.IsActive).AsQueryable();

            if (!string.IsNullOrEmpty(search.Title))
            {
                query = query.Where(x => x.Title.ToLower().Contains(search.Title.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.Genre))
            {
                query = query.Where(x => x.Genres.Any(g => g.Name == search.Genre));
            }

            if (!string.IsNullOrEmpty(search.Artist))
            {
                query = query.Where(x => x.Artists.Any(g => g.Name == search.Artist));
            }

            if (!string.IsNullOrEmpty(search.Album))
            {
                query = query.Where(x => x.Album != null && x.Album.Name == search.Album);
            }

            if (search.ReleaseDate.HasValue)
            {
                query = query.Where(x => x.ReleaseDate >= search.ReleaseDate);
            }

            if (!string.IsNullOrEmpty(search.Ids))
            {
                List<int>? ids = JsonConvert.DeserializeObject<List<int>>(search.Ids);
                if (ids != null && ids.Count > 0)
                {
                    query = query.Where(x => ids.Contains(x.Id));
                }
            }


            int totalCount = query.Count();
            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage.Value) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page.Value) : 1;

            int skip = perPage * (page - 1);

            query = query.Include(x => x.Album).Include(x => x.Artists).Include(x => x.Genres).Skip(skip).Take(perPage);


            return new PagedResponse<DetailedTrackDto>
            {
                Data = query.Select(x => _mapper.Map<DetailedTrackDto>(x)).ToList(),
                PerPage = perPage,
                CurrentPage = page,
                TotalCount = totalCount
            };

        }
    }
}
