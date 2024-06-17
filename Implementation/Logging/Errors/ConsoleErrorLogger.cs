using Application;
using Application.Logging;
using System.Text;

namespace API.ErrorLogging
{
    public class ConsoleErrorLogger : IErrorLogger
    {
        // public Guid Log(AppError error)
        // {
        //     var errorDate = DateTime.UtcNow;
        //     StringBuilder builder = new StringBuilder();
        //     builder.AppendLine("Error code: " + error.ErrorId.ToString());
        //     builder.AppendLine("User: " + error.Username != null ? error.Username : "/");
        //     builder.AppendLine("Error time:" + errorDate.ToLongDateString());
        //     builder.AppendLine("Ex message:" + error.Exception.Message);
        //     builder.AppendLine("Ex stack trace:");
        //     builder.AppendLine(error.Exception.StackTrace);
        //     Console.WriteLine(builder.ToString());

        //     return Guid.NewGuid();

        // }
        public Guid Log(Exception error, IApplicationActor actor)
        {
            throw new NotImplementedException();
        }
    }
}
