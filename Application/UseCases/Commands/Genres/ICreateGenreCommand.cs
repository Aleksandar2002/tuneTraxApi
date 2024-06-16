using System.Windows.Input;
using Application.DTO.Genre;
using Application.UseCases;

namespace Application.UseCases.Commands.Genres;

public interface ICreateGenreCommand : ICommand<CreateGenreDto>
{

}
