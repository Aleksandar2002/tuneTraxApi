using Application.Logging;
using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCaseHandling.QueryHandlers
{
    public class LoggingQueryHandler : IQueryHandler
    {
        private IQueryHandler _next;
        private IApplicationActor _actor;
        private IUseCaseLogger _logger;

        public LoggingQueryHandler(IQueryHandler next, IApplicationActor actor, IUseCaseLogger logger)
        {
            _next = next;
            _actor = actor;
            _logger = logger;
        }

        public TResult HandleQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search) where TResult : class
        {

            UseCaseLog log = new()
            {
                UseCaseName = query.Name,
                Username = _actor.Username,
                UseCaseData = search
            };

            _logger.Log(log);

            return _next.HandleQuery(query, search);
        }
    }
}
