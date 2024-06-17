using Application.DTO.Comment;
using DataAccess;
using FluentValidation;

namespace Implementation.Validators.Comment;

public class UpdateCommentValidator : AbstractValidator<UpdateCommentDto>
{
    public UpdateCommentValidator(TuneTraxContext context)
    {
        CascadeMode = CascadeMode.StopOnFirstFailure;
        RuleFor(x => x.Text).NotEmpty().WithMessage("Comment text is required").Matches("^[A-z0-9:?/:;.@!$%^&<>()\\s]{1,}$$").WithMessage("Wrong text format");
    }
}
