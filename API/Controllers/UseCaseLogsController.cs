using APi.Controllers;
using Application.UseCaseHandling.CommandHandlers;
using Application.UseCaseHandling.QueryHandlers;
using Application.UseCases.Queries.Logs;
using Application.UseCases.Queries.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UseCaseLogsController : BaseController
{
    public UseCaseLogsController(ICommandHandler commandHandler, IQueryHandler queryHandler) : base(commandHandler, queryHandler)
    {
    }


    [Authorize]
    [HttpGet]
    public IActionResult Get([FromServices] IGetUseCaseLogsQuery query, [FromQuery] UseCaseLogSearch search)
    {
        return Ok(QueryHandler.HandleQuery(query, search));
    }
}
