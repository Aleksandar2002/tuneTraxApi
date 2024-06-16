using DataAccess;

namespace Implementation.Validators.Artist;

public class UpdateArtistValidator : ArtistCommandsValidator
{
    public UpdateArtistValidator(TuneTraxContext context) : base(context)
    {
    }

}
