using Application;
using Application.DTO.Comment;
using Application.UseCases.Commands.Comments;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Comment;

namespace Implementation.UseCases.Commands.Comments;

public class EFCreateCommentCommand : EFUseCase, ICreateCommentCommand
{
    private CreateCommentValidator _validator;
    private IApplicationActor actor;
    public EFCreateCommentCommand(TuneTraxContext context, CreateCommentValidator validator, IApplicationActor actor) : base(context)
    {
        _validator = validator;
        this.actor = actor;
    }

    public string Name => "Create comment";

    public int Id => 24;

    public void Execute(CreateCommentDto request)
    {
        _validator.ValidateAndThrow(request);

        Context.Comments.Add(new Comment
        {
            Text = request.Text,
            UserId = actor.Id,
            TrackId = request.TrackId.Value,
            ParentId = request.ParentId.HasValue ? request.ParentId.Value : null
        });
        Context.SaveChanges();
    }
}
