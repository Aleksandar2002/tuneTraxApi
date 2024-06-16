using APi.Controllers;
using Application.DTO.Comment;
using Application.DTO.Track;
using Application.UseCaseHandling.CommandHandlers;
using Application.UseCaseHandling.QueryHandlers;
using Application.UseCases.Commands;
using Application.UseCases.Commands.Comments;
using Application.UseCases.Commands.Tracks;
using Application.UseCases.Queries;
using Application.UseCases.Queries.Comments;
using Application.UseCases.Queries.Searches;
using Application.UseCases.Queries.Searches.Track;
using Implementation.Validators.Track;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : BaseController
    {
        public TracksController(ICommandHandler commandHandler, IQueryHandler queryHandler) : base(commandHandler, queryHandler)
        {
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromServices] ISearchTracksQuery query, [FromQuery] TrackSearch search)
        {
            return Ok(QueryHandler.HandleQuery(query, search));

        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindTrackQuery query)
        {
            return Ok(QueryHandler.HandleQuery(query, id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateTrackDto dto, [FromServices] ICreateTrackCommand command)
        {
            CommandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateTrackDto dto, [FromServices] IUpdateTrackCommand command)
        {
            dto.Id = id;
            CommandHandler.HandleCommand(command, dto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteTrackCommand command)
        {
            CommandHandler.HandleCommand(command, id);
            return NoContent();
        }


        // Comments


        [Authorize]
        [HttpGet("{trackId}/comments")]
        public IActionResult Get([FromServices] IGetCommentsQuery query, [FromQuery] CommentSearch search, int trackId)
        {
            search.TrackId = trackId;
            return Ok(QueryHandler.HandleQuery(query, search));
        }


        [Authorize]
        [HttpPost("{trackId}/comments")]
        public IActionResult Post([FromBody] CreateCommentDto dto, [FromServices] ICreateCommentCommand command, int trackId)
        {
            dto.TrackId = trackId;
            CommandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

    }
}
