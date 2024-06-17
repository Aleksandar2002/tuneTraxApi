using Application.DTO.Track;
using Application.Exceptions;
using Application.UseCases.Commands.Tracks;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Track;
using Microsoft.EntityFrameworkCore;

namespace Implementation.UseCases.Commands.Tracks;

public class EFUpdateTrackCommand : EFUseCase, IUpdateTrackCommand
{
    public UpdateTrackValidator _validator;
    public IMapper _mapper;
    public EFUpdateTrackCommand(TuneTraxContext context, UpdateTrackValidator validator, IMapper mapper) : base(context)
    {
        _validator = validator;
        _mapper = mapper;
    }


    public string Name => "Update track";

    public int Id => 8;

    public void Execute(UpdateTrackDto request)
    {
        _validator.ValidateAndThrow(request);

        var track = Context.Tracks.Include(x => x.Artists).Include(x => x.Genres).FirstOrDefault(x => x.Id == request.Id && x.IsActive);

        if (track == null)
        {
            throw new EntityNotFoundException(request.Id.Value, nameof(Track));
        }

        if (!string.IsNullOrEmpty(request.AudioFile))
        {
            RemoveExistingFile(track.AudioFile, "trackAudioFiles");
            MoveFileToPermanentDestination(request.AudioFile, "trackAudioFiles");
            track.AudioFile = request.AudioFile;

        }

        if (!string.IsNullOrEmpty(request.CoverImage))
        {
            if (track.CoverImage != null)
                RemoveExistingFile(track.CoverImage, "trackCoverImages");
            MoveFileToPermanentDestination(request.CoverImage, "trackCoverImages");
            track.CoverImage = request.CoverImage;
        }

        track.Title = request.Title;
        track.AlbumId = request.AlbumId;
        track.Duration = request.Duration;
        track.ReleaseDate = request.ReleaseDate;
        // track.Genres = Context.Genres.Where(x => request.Genres.Contains(x.Id)).ToList();
        // track.Artists = Context.Artists.Where(x => request.Artists.Contains(x.Id)).ToList();



        var existingGenres = Context.GenreTrack.Where(x => x.TrackId == track.Id).ToList();
        Context.GenreTrack.RemoveRange(existingGenres);
        Context.GenreTrack.AddRange(request.Genres.Select(x => new GenreTrack
        {
            TrackId = track.Id,
            GenreId = x
        }));


        var existingArtists = Context.ArtistTrack.Where(x => x.TrackId == track.Id).ToList();
        Context.ArtistTrack.RemoveRange(existingArtists);
        Context.ArtistTrack.AddRange(request.Artists.Select(x => new ArtistTrack
        {
            TrackId = track.Id,
            ArtistId = x
        }));

        Context.SaveChanges();
    }

    public void RemoveExistingFile(string file, string folder)
    {
        var path = Path.Combine("wwwroot", folder, file);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public void MoveFileToPermanentDestination(string file, string destinationFolder)
    {
        var tempFile = Path.Combine("wwwroot", "temp", file);
        var destinationFile = Path.Combine("wwwroot", destinationFolder, file);
        File.Move(tempFile, destinationFile);
    }
}
