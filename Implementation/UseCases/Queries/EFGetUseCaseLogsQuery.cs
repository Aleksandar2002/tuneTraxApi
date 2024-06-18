using Application;
using Application.UseCases.Queries.Logs;
using Application.UseCases.Queries.Searches;
using DataAccess;
using Domain.Entities;

namespace Implementation.UseCases.Queries;

public class EFGetUseCaseLogsQuery : EFUseCase, IGetUseCaseLogsQuery
{
    public EFGetUseCaseLogsQuery(TuneTraxContext context) : base(context)
    {
    }


    public string Name => "Get use case logs";

    public int Id => 32;

    public PagedResponse<UseCaseLog> Execute(UseCaseLogSearch search)
    {
        var query = Context.UseCaseLogs.AsQueryable();

        if (!string.IsNullOrEmpty(search.Username))
        {
            query = query.Where(x => x.Username.ToLower().Contains(search.Username.ToLower()));
        }

        if (!string.IsNullOrEmpty(search.UseCaseName))
        {
            query = query.Where(x => x.UseCaseName.ToLower().Contains(search.UseCaseName.ToLower()));
        }

        if (search.DateFrom.HasValue)
        {
            query = query.Where(x => x.AttemptedAt >= search.DateFrom);
        }

        if (search.DateTo.HasValue)
        {
            query = query.Where(x => x.AttemptedAt <= search.DateTo);
        }

        var totalCount = query.Count();
        var page = search.Page.HasValue ? search.Page.Value : 1;
        var perPage = search.PerPage.HasValue ? search.PerPage.Value : 10;

        return new PagedResponse<UseCaseLog>
        {
            Data = query.ToList(),
            PerPage = perPage,
            CurrentPage = page,
            TotalCount = totalCount
        };
    }

}
