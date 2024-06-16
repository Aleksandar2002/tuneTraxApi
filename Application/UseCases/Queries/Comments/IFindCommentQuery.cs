using Application.DTO.Comment;

namespace Application.UseCases.Queries.Comments;

public interface IFindCommentQuery : IQuery<int, CommentDto>
{

}
