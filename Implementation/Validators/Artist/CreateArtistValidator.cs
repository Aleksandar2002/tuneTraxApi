using DataAccess;
using FluentValidation;


namespace Implementation.Validators.Artist
{
    public class CreateArtistValidator : ArtistCommandsValidator
    {
        public CreateArtistValidator(TuneTraxContext context) : base(context)
        {
            RuleFor(x => x.Name).Must(c => !context.Artists.Any(x => x.Name == c && x.IsActive)).WithMessage("Name already exists");
        }
    }
}
