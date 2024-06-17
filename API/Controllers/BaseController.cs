using Application.UseCaseHandling.CommandHandlers;
using Application.UseCaseHandling.QueryHandlers;
using Microsoft.AspNetCore.Mvc;

namespace APi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected ICommandHandler CommandHandler { get; }
        protected IQueryHandler QueryHandler { get; }

        protected BaseController(ICommandHandler commandHandler, IQueryHandler queryHandler)
        {
            CommandHandler = commandHandler;
            QueryHandler = queryHandler;
        }
    }
}

