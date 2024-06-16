using Application;
using Application.DTO.Album;
using Application.DTO.Artist;
using Application.DTO.Track;
using Application.UseCases.Queries.Artists;
using Application.UseCases.Queries.Searches;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EFGetArtistsQuery : EFUseCase, IGetArtistsQuery
    {
        private IMapper _mapper;
        public EFGetArtistsQuery(TuneTraxContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public string Name => "Get artists";

        public int Id => 13;

        public PagedResponse<DetailedArtistDto> Execute(ArtistSearch search)
        {
            var query = Context.Artists.Where(x => x.IsActive)
                .Include(x => x.Tracks)
                .Include(x => x.Country)
                .Include(x => x.Genres)
                .Include(x => x.Albums).AsQueryable();

            int totalCount = query.Count();
            int page = search.Page.HasValue ? search.Page.Value : 1;
            int perPage = search.PerPage.HasValue ? search.PerPage.Value : 10;


            var artists = query.Select(x => _mapper.Map<Artist, DetailedArtistDto>(x)).ToList();


            return new PagedResponse<DetailedArtistDto>
            {
                PerPage = perPage,
                CurrentPage = page,
                TotalCount = totalCount,
                Data = artists
            };
        }
    }
}
