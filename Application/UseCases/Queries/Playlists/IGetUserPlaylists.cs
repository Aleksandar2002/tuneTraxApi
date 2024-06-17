using Application.DTO.Playlist;

namespace Application.UseCases.Queries.Playlists;

public interface IGetUserPlaylists : IQuery<PlaylistSearch, PagedResponse<PlaylistDto>>
{

}
