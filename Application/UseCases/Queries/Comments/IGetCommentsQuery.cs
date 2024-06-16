using Application.DTO.Comment;
using Application.UseCases.Queries.Searches;

namespace Application.UseCases.Queries.Comments;

public interface IGetCommentsQuery : IQuery<CommentSearch, PagedResponse<CommentDto>>
{

}
