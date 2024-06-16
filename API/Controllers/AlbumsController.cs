using APi.Controllers;
using Application.DTO.Album;
using Application.UseCaseHandling.CommandHandlers;
using Application.UseCaseHandling.QueryHandlers;
using Application.UseCases.Commands.Albums;
using Application.UseCases.Queries.Albums;
using Application.UseCases.Queries.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class AlbumsController : BaseController
{
    public AlbumsController(ICommandHandler commandHandler, IQueryHandler queryHandler) : base(commandHandler, queryHandler)
    {
    }

    [Authorize]
    [HttpGet]
    public IActionResult Get([FromServices] IGetAlbumsQuery query, [FromQuery] AlbumSearch search)
    {
        return Ok(QueryHandler.HandleQuery(query, search));
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult Find([FromServices] IFindAlbumQuery query, int id)
    {
        return Ok(QueryHandler.HandleQuery(query, id));
    }

    [Authorize]
    [HttpPost]
    public IActionResult Post([FromServices] ICreateAlbumCommand command, [FromBody] CreateAlbumDto dto)
    {
        CommandHandler.HandleCommand(command, dto);
        return StatusCode(201);
    }


    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Put([FromServices] IUpdateAlbumCommand command, [FromBody] UpdateAlbumDto dto, int id)
    {
        dto.Id = id;
        CommandHandler.HandleCommand(command, dto);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete([FromServices] IDeleteAlbumCommand command, int id)
    {
        CommandHandler.HandleCommand(command, id);
        return NoContent();
    }

}
