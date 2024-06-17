using System.Xml;
using Application.DTO.Artist;
using Application.UseCases.Commands.Artists;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Artist;

namespace Implementation.UseCases.Commands.Artists
{
    public class EFCreateArtistCommand : EFUseCase, ICreateArtistCommand
    {
        private CreateArtistValidator _validator;
        private IMapper _mapper;
        public EFCreateArtistCommand(TuneTraxContext context, CreateArtistValidator validator, IMapper mapper) : base(context)
        {
            _validator = validator;
            _mapper = mapper;
        }

        public string Name => "Create artist";

        public int Id => 14;

        public void Execute(CreateArtistDto request)
        {
            _validator.ValidateAndThrow(request);

            if (request.Image != null)
            {
                var tempFile = Path.Combine("wwwroot", "temp", request.Image);
                var destinationFile = Path.Combine("wwwroot", "artists", request.Image);
                File.Move(tempFile, destinationFile);
            }

            var artist = new Artist
            {
                Name = request.Name,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CountryId = request.CountryId,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Genres = Context.Genres.Where(x => request.Genres.Contains(x.Id) && x.IsActive).ToList()
            };

            // var artist2 = _mapper.Map<CreateArtistDto, Artist>(request);

            Context.Artists.Add(artist);
            Context.SaveChanges();
        }
    }
}
