using Application.DTO.Track;
using Application.UseCases.Commands;
using Azure.Core;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Track;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Tracks
{
    public class EFCreateTrackCommand : EFUseCase, ICreateTrackCommand
    {
        private CreateTrackValidator _validator;

        public EFCreateTrackCommand(CreateTrackValidator validator, TuneTraxContext context) : base(context)
        {
            _validator = validator;
        }

        public string Name => "Create track";

        public int Id => 6;

        public void Execute(CreateTrackDto request)
        {
            _validator.ValidateAndThrow(request);

            MoveFileToPermanentDestination(request.AudioFile, "trackAudioFiles");

            if (request.CoverImage != null)
            {
                MoveFileToPermanentDestination(request.CoverImage, "trackCoverImages");
            }


            Context.Tracks.Add(new Track
            {
                Title = request.Title,
                ReleaseDate = request.ReleaseDate,
                Duration = request.Duration,
                AudioFile = request.AudioFile,
                CoverImage = request.CoverImage,
                Genres = Context.Genres.Where(g => request.Genres.Contains(g.Id) && g.IsActive).ToList(),
                Artists = Context.Artists.Where(a => request.Genres.Contains(a.Id) && a.IsActive).ToList(),
                AlbumId = request.AlbumId,
            });
            Context.SaveChanges();
        }

        public void MoveFileToPermanentDestination(string file, string destinationFolder)
        {
            var tempFile = Path.Combine("wwwroot", "temp", file);
            var destinationFile = Path.Combine("wwwroot", destinationFolder, file);
            File.Move(tempFile, destinationFile);
        }
    }
}
