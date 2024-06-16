using Application.DTO.Comment;
using AutoMapper;
using Domain.Entities;

namespace Implementation.Mappers;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>()
        .ForMember(d => d.Username, x => x.MapFrom(s => s.User.Username))
        .ForMember(d => d.Children, x => x.MapFrom((s, dest, _, context) => MapChildren(s.Children, context)));
    }

    private List<CommentDto> MapChildren(IEnumerable<Comment> comments, ResolutionContext context)
    {
        if (comments == null || !comments.Any())
            return null;

        var children = new List<CommentDto>();

        foreach (var comment in comments)
        {
            var commentDto = context.Mapper.Map<CommentDto>(comment); ;


            children.Add(commentDto);
        }

        return children;
    }

}
