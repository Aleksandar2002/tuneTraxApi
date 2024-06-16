using Application.DTO.Comment;
using Application.Exceptions;
using Application.UseCases.Commands.Comments;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Comment;

namespace Implementation.UseCases.Commands.Comments;

public class EFUpdateCommentCommand : EFUseCase, IUpdateCommentCommand
{
    private UpdateCommentValidator _validator;
    public EFUpdateCommentCommand(TuneTraxContext context, UpdateCommentValidator validator) : base(context)
    {
        _validator = validator;

    }


    public string Name => "Update comment";

    public int Id => 26;

    public void Execute(UpdateCommentDto request)
    {
        _validator.ValidateAndThrow(request);

        var comment = Context.Comments.Find(request.Id);

        if (comment == null)
        {
            throw new EntityNotFoundException(request.Id, typeof(Comment).Name);
        }

        comment.Text = request.Text;
        Context.SaveChanges();
    }

}
