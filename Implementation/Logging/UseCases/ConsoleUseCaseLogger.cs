using Application.Logging;
using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Implementation.Logging.UseCases
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public void Log(UseCaseLog log)
        {

            Console.WriteLine($"Date {log.AttemptedAt.ToLongDateString() + " " + log.AttemptedAt.ToLongTimeString()}, User: {log.Username}, Usecase: {log.UseCaseName}, Usecase data: {log.UseCaseData}");
        }
    }
}
