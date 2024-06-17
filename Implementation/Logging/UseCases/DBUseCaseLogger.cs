using Application.Logging;
using DataAccess;
using Domain.Entities;

namespace Implementation.Logging.UseCases;

public class DBUseCaseLogger : IUseCaseLogger
{
    private TuneTraxContext _context;

    public DBUseCaseLogger(TuneTraxContext context)
    {
        _context = context;
    }


    public void Log(UseCaseLog log)
    {
        _context.UseCaseLogs.Add(log);
        _context.SaveChanges();
    }

}
