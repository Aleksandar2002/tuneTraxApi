using Application;
using Application.Exceptions;
using Application.UseCases.Commands.Comments;
using DataAccess;
using Domain.Entities;

namespace Implementation.UseCases.Commands.Comments;

public class EFDeleteCommentCommand : EFUseCase, IDeleteCommentCommand
{
    private IApplicationActor _actor;
    public EFDeleteCommentCommand(TuneTraxContext context, IApplicationActor actor) : base(context)
    {
        _actor = actor;

    }

    public string Name => "Delete comment";

    public int Id => 25;

    public void Execute(int request)
    {
        var comment = Context.Comments.Find(request);

        if (comment == null || !comment.IsActive || comment.DeletedAt.HasValue)
        {
            throw new EntityNotFoundException(request, typeof(Comment).Name);
        }

        if (_actor.Id != comment.UserId)
        {
            throw new InvalidOperationException("Only author of the comment can delete it");
        }

        // DELETE CHILDREN COMMENTS
        DeleteCommentAndChildren(comment);

        // comment.IsActive = false;
        // comment.DeletedAt = DateTime.UtcNow;
        Context.Comments.Remove(comment);
        Context.SaveChanges();
    }

    private void DeleteCommentAndChildren(Comment comment)
    {
        foreach (var child in comment.Children.ToList())
        {
            DeleteCommentAndChildren(child);

            if (child.IsActive && !child.DeletedAt.HasValue)
            {
                Context.Comments.Remove(child);
            }
        }
    }
}
