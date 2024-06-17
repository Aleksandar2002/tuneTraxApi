using Application.Exceptions;
using Application.UseCases.Commands;
using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Tracks
{
    public class EFDeleteTrackCommand : EFUseCase, IDeleteTrackCommand
    {
        public EFDeleteTrackCommand(TuneTraxContext context) : base(context)
        {
        }

        public string Name => "Delete track";

        public int Id => 7;

        public void Execute(int request)
        {
            var track = Context.Tracks.Find(request);
            if (track == null || !track.IsActive || track.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(request, typeof(Track).Name);
            }


            RemoveFile(track.AudioFile, "trackAudioFiles");
            if (track.CoverImage != null)
            {
                RemoveFile(track.CoverImage, "trackCoverImages");
            }


            var existingArtists = Context.ArtistTrack.Where(x => x.TrackId == track.Id).ToList();
            Context.ArtistTrack.RemoveRange(existingArtists);

            var existingGenres = Context.GenreTrack.Where(x => x.TrackId == track.Id).ToList();
            Context.GenreTrack.RemoveRange(existingGenres);

            var existingPlaylists = Context.PlaylistTrack.Where(x => x.TrackId == track.Id).ToList();
            Context.PlaylistTrack.RemoveRange(existingPlaylists);

            track.IsActive = false;
            track.DeletedAt = DateTime.UtcNow;

            Context.SaveChanges();
        }

        public void RemoveFile(string filePath, string fileFolder)
        {
            var path = Path.Combine("wwwroot", fileFolder, filePath);
            if (!Path.Exists(path))
            {
                throw new FileNotFoundException();
            }
            System.IO.File.Delete(path);
        }
    }
}
