using APi.Controllers;
using Application;
using Application.DTO.Playlist;
using Application.UseCaseHandling.CommandHandlers;
using Application.UseCaseHandling.QueryHandlers;
using Application.UseCases.Commands.Playlists;
using Application.UseCases.Queries.Playlists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlaylistsController : BaseController
{
    public PlaylistsController(ICommandHandler commandHandler, IQueryHandler queryHandler) : base(commandHandler, queryHandler)
    {
    }

    [Authorize]
    [HttpGet]
    public IActionResult Get([FromServices] IGetUserPlaylists query, [FromQuery] PlaylistSearch search)
    {
        return Ok(QueryHandler.HandleQuery(query, search));
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult Find([FromServices] IFindPlaylistQuery query, int id)
    {
        return Ok(QueryHandler.HandleQuery(query, id));
    }

    [Authorize]
    [HttpPost]
    public IActionResult Post([FromBody] CreatePlaylistDto dto, [FromServices] ICreatePlaylistCommand command)
    {
        CommandHandler.HandleCommand(command, dto);
        return StatusCode(201);
    }

    [Authorize]
    [HttpPost("{playlistId}/addTracks")]
    public IActionResult AddTrackToPlaylist([FromBody] PlaylistTracksDto dto, [FromServices] IAddTracksToPlaylistCommand command, int playlistId)
    {
        dto.PlaylistId = playlistId;
        CommandHandler.HandleCommand(command, dto);
        return StatusCode(201);
    }

    [Authorize]
    [HttpDelete("{playlistId}/deleteTracks")]
    public IActionResult AddTrackToPlaylist([FromBody] PlaylistTracksDto dto, [FromServices] IRemoveTracksFromPlaylist command, int playlistId)
    {
        dto.PlaylistId = playlistId;
        CommandHandler.HandleCommand(command, dto);
        return NoContent();
    }


    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Put([FromServices] IUpdatePlaylistCommand command, int id, [FromBody] UpdatePlaylistDto dto)
    {
        dto.Id = id;
        CommandHandler.HandleCommand(command, dto);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete([FromServices] IDeletePlaylistCommand command, int id)
    {
        CommandHandler.HandleCommand(command, id);
        return NoContent();
    }
}
