using Application.DTO.Track;
using DataAccess;
using FluentValidation;

namespace Implementation.Validators.Track;

public class TrackCommandsBaseValidator : AbstractValidator<CreateTrackDto>
{
    public TrackCommandsBaseValidator(TuneTraxContext context)
    {
        CascadeMode = CascadeMode.StopOnFirstFailure;
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required").Matches("^[a-zA-Z0-9\\s\\-_'&()!?,.]+$").WithMessage("Wrong title format ( can contains letters, numbers and some symbols)");

        RuleFor(x => x.Duration).NotEmpty().WithMessage("Duration is required").GreaterThan(0).WithMessage("Duration must be greater than 0").LessThan(30000).WithMessage("Duration must be less than 30000");

        RuleFor(x => x.ReleaseDate).NotEmpty().WithMessage("Release date is required").Must(d => d > new DateTime(1000, 1, 1)).WithMessage("Relase date must be after 1000/01/01.");

        RuleFor(x => x.AlbumId).Must(id => context.Albums.Any(a => a.Id == id && a.IsActive)).WithMessage("Album does not exist in database").When(x => x.AlbumId.HasValue);

        RuleFor(x => x.CoverImage).Matches(".(jpg|jpeg|png)$").WithMessage("Wrong extension").Must(x =>
        {
            string path = Path.Combine("wwwroot", "temp", x);
            return Path.Exists(path);
        }).WithMessage("File does not exists").When(x => !string.IsNullOrEmpty(x.CoverImage));

        RuleFor(x => x.Artists).Must(x => x != null && x.Count > 0).WithMessage("Artists are required")
                            .Must(x =>
                            {
                                var existingArtists = context.Artists.Where(ar => x.Contains(ar.Id) && ar.IsActive).Select(a => a.Id).ToList();
                                return existingArtists.Count == x.Count;
                            }).WithMessage("Some of the artist does not exist in database");


        RuleFor(x => x.Genres).Must(x => x != null && x.Count > 0).WithMessage("Genres are required")
            .Must(x =>
            {
                var existingGenres = context.Genres.Where(g => x.Contains(g.Id) && g.IsActive).Select(g => g.Id).ToList();
                return existingGenres.Count == x.Count;
            }).WithMessage("Some of the genres does not exist in database");
    }

}
