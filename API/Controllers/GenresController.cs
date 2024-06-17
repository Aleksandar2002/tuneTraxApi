using System.Runtime.CompilerServices;
using APi.Controllers;
using Application;
using Application.DTO.Genre;
using Application.UseCaseHandling.CommandHandlers;
using Application.UseCaseHandling.QueryHandlers;
using Application.UseCases.Commands.Genres;
using Application.UseCases.Queries.Genres;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : BaseController
    {
        public GenresController(ICommandHandler commandHandler, IQueryHandler queryHandler) : base(commandHandler, queryHandler)
        {
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromServices] IGetGenresQuery query, [FromQuery] GenreSearch search)
        {
            return Ok(QueryHandler.HandleQuery(query, search));
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Find([FromServices] IFindGenreQuery query, int id)
        {
            return Ok(QueryHandler.HandleQuery(query, id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromServices] ICreateGenreCommand command, [FromBody] CreateGenreDto dto)
        {
            CommandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }


        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put([FromServices] IUpdateGenreCommand command, [FromBody] UpdateGenreDto dto, int id)
        {
            dto.Id = id;
            CommandHandler.HandleCommand(command, dto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromServices] IDeleteGenreCommand command, int id)
        {
            CommandHandler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
