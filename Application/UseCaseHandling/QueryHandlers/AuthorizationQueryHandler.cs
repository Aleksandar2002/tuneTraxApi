using Application.Exceptions;
using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCaseHandling.QueryHandlers
{
    public class AuthorizationQueryHandler : IQueryHandler
    {
        private IQueryHandler _next;
        private IApplicationActor _actor;

        public AuthorizationQueryHandler(IQueryHandler next, IApplicationActor actor)
        {
            _next = next;
            _actor = actor;
        }

        public TResult HandleQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search) where TResult : class
        {
            if (!_actor.AllowedUseCases.Contains(query.Id))
            {
                throw new UnauthorizedUseCaseExecutionException(_actor.Username, query.Name);
            }
            return _next.HandleQuery(query, search);
        }
    }
}
