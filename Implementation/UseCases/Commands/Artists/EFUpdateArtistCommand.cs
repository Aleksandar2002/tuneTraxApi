using Application.DTO.Artist;
using Application.Exceptions;
using Application.UseCases.Commands.Artists;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Artist;

namespace Implementation.UseCases.Commands.Artists;

public class EFUpdateArtistCommand : EFUseCase, IUpdateArtistCommand
{
    private UpdateArtistValidator _validator;
    private IMapper _mapper;
    public EFUpdateArtistCommand(TuneTraxContext context, UpdateArtistValidator validator, IMapper mapper) : base(context)
    {
        _validator = validator;
        _mapper = mapper;
    }

    public string Name => "Update artist";

    public int Id => 31;

    public void Execute(UpdateArtistDto request)
    {
        _validator.ValidateAndThrow(request);

        var artist = Context.Artists.FirstOrDefault(x => x.Id == request.Id && x.IsActive);

        if (artist == null)
        {
            throw new EntityNotFoundException(request.Id.Value, nameof(Artist));
        }

        if (!string.IsNullOrEmpty(request.Image))
        {
            if (artist.Image != null)
                RemoveExistingImage(artist.Image, "artists");

            MoveFromTempFolderToArtistFolder(request.Image);
            artist.Image = request.Image;
        }

        artist.Name = request.Name;
        artist.CountryId = request.CountryId;
        artist.DateOfBirth = request.DateOfBirth;
        artist.LastName = request.LastName;
        artist.FirstName = request.FirstName;
        artist.Gender = request.Gender;

        var existingGenres = Context.ArtistGenre.Where(x => x.ArtistId == artist.Id).ToList();
        Context.ArtistGenre.RemoveRange(existingGenres);

        Context.ArtistGenre.AddRange(request.Genres.Select(x => new ArtistGenre
        {
            GenreId = x,
            ArtistId = artist.Id
        }));

        // _mapper.Map(request, artist);


        Context.SaveChanges();
    }

    public void RemoveExistingImage(string file, string folder)
    {
        string path = Path.Combine("wwwroot", folder, file);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public void MoveFromTempFolderToArtistFolder(string file)
    {
        var tempFile = Path.Combine("wwwroot", "temp", file);
        var destinationFile = Path.Combine("wwwroot", "artists", file);
        File.Move(tempFile, destinationFile);
    }
}
