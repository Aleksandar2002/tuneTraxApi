using System.Security.Permissions;
using APi.Controllers;
using Application;
using Application.DTO.Artist;
using Application.UseCaseHandling.CommandHandlers;
using Application.UseCaseHandling.QueryHandlers;
using Application.UseCases.Commands.Artists;
using Application.UseCases.Queries.Artists;
using Application.UseCases.Queries.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : BaseController
    {

        public ArtistsController(ICommandHandler commandHandler, IQueryHandler queryHandler) : base(commandHandler, queryHandler)
        {
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromServices] IGetArtistsQuery query, [FromQuery] ArtistSearch search)
        {
            return Ok(QueryHandler.HandleQuery(query, search));
        }


        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Find([FromServices] IFindArtistQuery query, int id)
        {
            return Ok(QueryHandler.HandleQuery(query, id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromServices] ICreateArtistCommand command, [FromBody] CreateArtistDto dto, [FromServices] IApplicationActor actor)
        {
            CommandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }


        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put([FromServices] IUpdateArtistCommand command, int id, [FromBody] UpdateArtistDto dto)
        {
            dto.Id = id;
            CommandHandler.HandleCommand(command, dto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromServices] IDeleteArtistCommand command, int id)
        {
            CommandHandler.HandleCommand(command, id);
            return NoContent();
        }


    }
}
