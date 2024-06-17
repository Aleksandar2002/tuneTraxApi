using Application.DTO.Comment;
using DataAccess;
using FluentValidation;

namespace Implementation.Validators.Comment;

public class CreateCommentValidator : AbstractValidator<CreateCommentDto>
{
    public CreateCommentValidator(TuneTraxContext context)
    {
        CascadeMode = CascadeMode.StopOnFirstFailure;
        RuleFor(x => x.Text).NotEmpty().WithMessage("Comment text is required").Matches("^[A-z0-9:?/:;.@!$%^&<>()\\s]{1,}$").WithMessage("Wrong text format");
        RuleFor(x => x.TrackId).Must(x => context.Tracks.Any(t => t.Id == x && t.IsActive)).WithMessage("Track does not exist");

        RuleFor(x => x.ParentId).Must(id => context.Comments.Any(x => x.Id == id && x.IsActive)).WithMessage("Parent comment does not exist").When(x => x.ParentId != null);
    }

}
