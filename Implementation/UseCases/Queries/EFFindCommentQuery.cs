using Application.DTO.Comment;
using Application.UseCases.Queries.Comments;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Implementation.UseCases.Generics;

namespace Implementation.UseCases.Queries;

public class EFFindCommentQuery : GenericFind<Comment, CommentDto>, IFindCommentQuery
{
    public EFFindCommentQuery(TuneTraxContext context, IMapper mapper) : base(context, mapper)
    {
    }


    public override string Name => "Find comment";

    public override int Id => 34;

}
