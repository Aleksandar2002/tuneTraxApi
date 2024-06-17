using Application.UseCases.Queries.Searches;
using Domain.Entities;

namespace Application.UseCases.Queries.Logs;

public interface IGetUseCaseLogsQuery : IQuery<UseCaseLogSearch, PagedResponse<UseCaseLog>>
{

}
