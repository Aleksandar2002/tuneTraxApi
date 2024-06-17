using Application.DTO.Artist;
using DataAccess;
using Domain.Entities;
using FluentValidation;

namespace Implementation.Validators.Artist;

public class ArtistCommandsValidator : AbstractValidator<CreateArtistDto>
{
    public ArtistCommandsValidator(TuneTraxContext context)
    {
        CascadeMode = CascadeMode.StopOnFirstFailure;
        RuleFor(x => x.CountryId).NotEmpty().WithMessage("Country birth is required").Must(c => context.Countries.Any(x => x.Id == c)).WithMessage("Country does not exist");


        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth is required").Must(d => DateTime.UtcNow.Year - d.Year >= 10).WithMessage("Artist must be older than 10 years");

        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required").Matches("^[A-Z][a-z]{2,40}$").WithMessage("Wrong first name format");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required").Matches("^[A-Z][a-z]{2,40}$").WithMessage("Wrong last name format");



        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").Matches("^[A-z0-9_@$\\-\\s]{6,}$").WithMessage("Wrong name format, numbers, letters and some symbols are allowed ( \" -$@- \"), 6 characters minimum");

        RuleFor(x => x.Gender).Must(g => Enum.IsDefined(typeof(Gender), g)).WithMessage("Gender have not valid value ( only 0 (m) or 1 (f) ) ");

        RuleFor(x => x.Image).Matches(".(jpg|jpeg|png)$").WithMessage("Wrong extension").Must(x =>
        {
            string path = Path.Combine("wwwroot", "temp", x);
            return Path.Exists(path);
        }).WithMessage("File does not exists").When(x => !string.IsNullOrEmpty(x.Image));

        RuleFor(x => x.Genres).Must(x => x != null && x.Count > 0).WithMessage("Genres are required")
            .Must(x =>
            {
                var existingGenres = context.Genres.Where(g => x.Contains(g.Id) && g.IsActive).Select(g => g.Id).ToList();
                return existingGenres.Count == x.Count;
            }).WithMessage("Some of the genres does not exist in database");
    }

}
