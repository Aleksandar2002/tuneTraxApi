using APi.Controllers;
using Application;
using Application.DTO;
using Application.DTO.User;
using Application.UseCaseHandling.CommandHandlers;
using Application.UseCaseHandling.QueryHandlers;
using Application.UseCases.Commands;
using Application.UseCases.Commands.Users;
using Application.UseCases.Queries.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(ICommandHandler commandHandler, IQueryHandler queryHandler) : base(commandHandler, queryHandler)
        {
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromServices] IGetUsersQuery query, [FromQuery] UserSearch search)
        {
            return Ok(QueryHandler.HandleQuery(query, search));
        }


        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Find([FromServices] IFindUserQuery query, int id)
        {
            return Ok(QueryHandler.HandleQuery(query, id));
        }


        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserDto dto, [FromServices] IRegisterUserCommand command)
        {
            CommandHandler.HandleCommand(command, dto);
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}/privileges")]
        public IActionResult UpdateUserPrivileges([FromServices] IUpdateUserPrivilegesCommand command, UpdateUserPrivilegesDto dto, int id)
        {
            dto.UserId = id;
            CommandHandler.HandleCommand(command, dto);
            return NoContent();
        }


        // [Authorize]
        // [HttpPut("{id}")]
        // public IActionResult Put([FromServices] IUpdateUserCommand command, UpdateUserDto dto, int id)
        // {
        //     dto.UserId = id;
        //     CommandHandler.HandleCommand(command, dto);
        //     return NoContent();
        // }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromServices] IDeleteUserCommand command, int id)
        {
            CommandHandler.HandleCommand(command, id);
            return NoContent();
        }
    }
}

