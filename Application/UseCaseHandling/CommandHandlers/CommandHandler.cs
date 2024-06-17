using Application.Exceptions;
using Application.Logging;
using Application.UseCases;
using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.UseCaseHandling.CommandHandlers
{
    public class CommandHandler : ICommandHandler
    {
        private IApplicationActor _actor;
        private IUseCaseLogger _logger;

        public CommandHandler(IApplicationActor actor, IUseCaseLogger logger)
        {
            _actor = actor;
            _logger = logger;
        }

        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest data)
        {
            if (!_actor.AllowedUseCases.Contains(command.Id))
            {
                throw new UnauthorizedUseCaseExecutionException(_actor.Username, command.Name);
            }

            _logger.Log(new UseCaseLog
            {
                Username = _actor.Username,
                UseCaseData = JsonConvert.SerializeObject(data),
                UseCaseName = command.Name,
                AttemptedAt = DateTime.UtcNow
            });

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            command.Execute(data);
            stopwatch.Stop();

            Console.WriteLine($"Usecase {command.Name}, Time: {stopwatch.ElapsedMilliseconds}ms, User : {_actor.Username}");
        }
    }
}
