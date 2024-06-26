﻿using Application.DTO.Artist;
using Application.UseCases.Queries.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Artists
{
    public interface IGetArtistsQuery: IQuery<ArtistSearch ,PagedResponse<DetailedArtistDto>>
    {
    }
}
