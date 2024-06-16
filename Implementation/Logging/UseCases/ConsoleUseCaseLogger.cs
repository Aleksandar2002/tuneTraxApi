using Application.Logging;
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
            DateTime date = DateTime.UtcNow;
            string username = log.Username;
            string useCaseName= log.UseCaseName;
            string useCaseData= JsonConvert.SerializeObject(log.UseCaseData);

            Console.WriteLine($"Date {date.ToLongDateString() + " " + date.ToLongTimeString()}, User: {username}, Usecase: {useCaseName}, Usecase data: {useCaseData}");
        }
    }
}
