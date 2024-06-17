using Application;
using Application.Logging;
using DataAccess;
using Domain.Entities;

namespace API.ErrorLogging;

public class DBErrorLogger : IErrorLogger
{
    private TuneTraxContext _context;

    public DBErrorLogger(TuneTraxContext context)
    {
        _context = context;
    }


    public Guid Log(Exception error, IApplicationActor actor)
    {
        ErrorLog log = new()
        {
            ErrorId = Guid.NewGuid(),
            StackTrace = error.StackTrace,
            Message = error.Message,
            Time = DateTime.UtcNow,
            ActorUsername = actor.Username
        };

        _context.ErrorLogs.Add(log);
        _context.SaveChanges();

        return log.ErrorId;
    }
}