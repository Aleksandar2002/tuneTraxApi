using APi.Controllers;
using Application.DTO.Comment;
using Application.UseCaseHandling.CommandHandlers;
using Application.UseCaseHandling.QueryHandlers;
using Application.UseCases.Commands.Comments;
using Application.UseCases.Queries.Comments;
using Application.UseCases.Queries.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : BaseController
{
    public CommentsController(ICommandHandler commandHandler, IQueryHandler queryHandler) : base(commandHandler, queryHandler)
    {
    }


    [Authorize]
    [HttpGet("{id}")]
    public IActionResult Find([FromServices] IFindCommentQuery query, int id)
    {
        return Ok(QueryHandler.HandleQuery(query, id));
    }


    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Put([FromServices] IUpdateCommentCommand command, int id, [FromBody] UpdateCommentDto dto)
    {
        dto.Id = id;
        CommandHandler.HandleCommand(command, dto);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete([FromServices] IDeleteCommentCommand command, int id)
    {
        CommandHandler.HandleCommand(command, id);
        return NoContent();
    }

}


