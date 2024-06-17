using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class UnauthorizedUseCaseExecutionException : Exception
    {
        public UnauthorizedUseCaseExecutionException(string username, string useCaseName) :base($"Unauthorized access attempt by {username} for usecase: {useCaseName}")
        {
        }
    }
}
