using System.Data;
using Application.DTO.Album;
using DataAccess;
using FluentValidation;

namespace Implementation.Validators.Albums;

public class CreateUpdateAlbumValidator : AbstractValidator<CreateAlbumDto>
{
    public CreateUpdateAlbumValidator(TuneTraxContext context)
    {
        RuleFor(x => x.ArtistId).Must(id => context.Artists.Any(x => x.Id == id && x.IsActive)).WithMessage("Artist does not exist");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").Matches("^[A-z0-9-_.,/\\s\\.]{1,}$").WithMessage("Wrong album name format");

        RuleFor(x => x.ReleaseDate).NotEmpty().WithMessage("Released date is required");
    }
}
