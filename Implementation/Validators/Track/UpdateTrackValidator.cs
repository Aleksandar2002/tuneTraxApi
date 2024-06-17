using System.Data;
using DataAccess;
using FluentValidation;

namespace Implementation.Validators.Track;

public class UpdateTrackValidator : TrackCommandsBaseValidator
{
    public UpdateTrackValidator(TuneTraxContext context) : base(context)
    {
        RuleFor(x => x.AudioFile).NotEmpty().WithMessage("Audio file is required").Matches(".(mp3|wav|flac|aac|ogg|m4a)$").WithMessage("Wrong audio file format").Must(x =>
        {
            string path = Path.Combine("wwwroot", "temp", x);
            return Path.Exists(path);
        }).WithMessage("File does not exists").When(x => !string.IsNullOrEmpty(x.AudioFile));
    }

}
