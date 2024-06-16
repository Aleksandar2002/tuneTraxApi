using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.ErrorLogging;
using API.JWT;
using API.JWT.TokenStorage;
using Application;
using Application.Logging;
using Application.UseCases.Commands;
using Application.UseCases.Commands.Albums;
using Application.UseCases.Commands.Artists;
using Application.UseCases.Commands.Comments;
using Application.UseCases.Commands.Genres;
using Application.UseCases.Commands.Playlists;
using Application.UseCases.Commands.Tracks;
using Application.UseCases.Commands.Users;
using Application.UseCases.Queries;
using Application.UseCases.Queries.Albums;
using Application.UseCases.Queries.Artists;
using Application.UseCases.Queries.Comments;
using Application.UseCases.Queries.Genres;
using Application.UseCases.Queries.Playlists;
using Application.UseCases.Queries.Users;
using DataAccess;
using Implementation;
using Implementation.Logging.UseCases;
using Implementation.UseCases.Commands;
using Implementation.UseCases.Commands.Albums;
using Implementation.UseCases.Commands.Artists;
using Implementation.UseCases.Commands.Comments;
using Implementation.UseCases.Commands.Genres;
using Implementation.UseCases.Commands.Playlists;
using Implementation.UseCases.Commands.Tracks;
using Implementation.UseCases.Commands.Users;
using Implementation.UseCases.Queries;
using Implementation.Validators;
using Implementation.Validators.Albums;
using Implementation.Validators.Artist;
using Implementation.Validators.Comment;
using Implementation.Validators.Genre;
using Implementation.Validators.Playlist;
using Implementation.Validators.Track;
using Implementation.Validators.Users;

namespace API.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddLoggers(this IServiceCollection services)
        {

            services.AddTransient<IUseCaseLogger, ConsoleUseCaseLogger>();
            services.AddTransient<IErrorLogger, DBErrorLogger>();


            // services.AddTransient<IErrorLogger>(x=>
            // {
            //     var accessor = x.GetService<IHttpContextAccessor>();

            //     if (accessor == null || accessor.HttpContext == null) return new ConsoleErrorLogger();

            //     var logger = accessor.HttpContext.Request.Headers["Logger"].FirstOrDefault();
            //     if(logger == "Console")
            //     {
            //         return new ConsoleErrorLogger();
            //     }
            //     else
            //     {
            //         return new DBErrorLogger();
            //     }
            // });
        }
        public static void AddUseCaseCommands(this IServiceCollection services)
        {

            //------------------------- COMMANDS AND VALIDATORS


            services.AddTransient<IRegisterUserCommand, EFRegisterUserCommand>();
            services.AddTransient<IUpdateUserPrivilegesCommand, EFUpdateUserPrivilegesCommand>();

            // ------- Tracks

            services.AddTransient<ICreateTrackCommand, EFCreateTrackCommand>();
            services.AddTransient<IDeleteTrackCommand, EFDeleteTrackCommand>();
            services.AddTransient<IUpdateTrackCommand, EFUpdateTrackCommand>();

            // ------ GENRES

            services.AddTransient<ICreateGenreCommand, EFCreateGenreCommand>();
            services.AddTransient<IDeleteGenreCommand, EFDeleteGenreCommand>();
            services.AddTransient<IUpdateGenreCommand, EFUpdateGenreCommand>();

            // ------- Artists

            services.AddTransient<ICreateArtistCommand, EFCreateArtistCommand>();
            services.AddTransient<IDeleteArtistCommand, EFDeleteArtistCommand>();
            services.AddTransient<IUpdateArtistCommand, EFUpdateArtistCommand>();



            // ----------- Playlists

            services.AddTransient<ICreatePlaylistCommand, EFCreatePlaylistCommand>();
            services.AddTransient<IUpdatePlaylistCommand, EFUpdatePlaylistCommand>();
            services.AddTransient<IDeletePlaylistCommand, EFDeletePlaylistCommand>();

            services.AddTransient<IAddTracksToPlaylistCommand, EFAddTrackToPlaylistCommand>();
            services.AddTransient<IRemoveTracksFromPlaylist, EFRemoveTracksFromPlaylist>();


            // ------------------ Comments

            services.AddTransient<ICreateCommentCommand, EFCreateCommentCommand>();
            services.AddTransient<IDeleteCommentCommand, EFDeleteCommentCommand>();
            services.AddTransient<IUpdateCommentCommand, EFUpdateCommentCommand>();


            //-------------------------- Albums

            services.AddTransient<ICreateAlbumCommand, EFCreateAlbumCommand>();
            services.AddTransient<IUpdateAlbumCommand, EFUpdateAlbumCommand>();
            services.AddTransient<IDeleteAlbumCommand, EFDeleteAlbumCommand>();


        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<UpdateUserPrivilegesValidator>();

            // Tracks

            services.AddTransient<CreateTrackValidator>();
            services.AddTransient<UpdateTrackValidator>();

            // Genres

            services.AddTransient<CreateGenreValidator>();
            services.AddTransient<UpdateGenreValidator>();

            // Artists

            services.AddTransient<CreateArtistValidator>();
            services.AddTransient<UpdateArtistValidator>();


            // Playlists

            services.AddTransient<CreatePlaylistValidator>();
            services.AddTransient<UpdatePlaylistValidator>();

            services.AddTransient<ChangeTracksInPlaylistValidator>();

            // Comments

            services.AddTransient<CreateCommentValidator>();
            services.AddTransient<UpdateCommentValidator>();

            // Albums

            services.AddTransient<CreateUpdateAlbumValidator>();

        }

        public static void AddUseCaseQueries(this IServiceCollection services)
        {
            // --------------------------- QUERIES

            services.AddTransient<ISearchTracksQuery, EFSearchTracksQuery>();
            services.AddTransient<IFindTrackQuery, EFFindTrackQuery>();

            // -------- Genres

            services.AddTransient<IGetGenresQuery, EFGetGenresQuery>();
            services.AddTransient<IFindGenreQuery, EFFindGenreQuery>();

            // ---------- Artists

            services.AddTransient<IGetArtistsQuery, EFGetArtistsQuery>();
            services.AddTransient<IFindArtistQuery, EFFindArtistQuery>();

            // ----------- Users

            services.AddTransient<IGetUsersQuery, EFGetUsersQuery>();
            services.AddTransient<IFindUserQuery, EFFindUserQuery>();


            // ----------- Playlists

            services.AddTransient<IGetUserPlaylists, EFGetUserPlaylists>();
            services.AddTransient<IFindPlaylistQuery, EFFindPlaylistQuery>();

            // ----------- Comments

            services.AddTransient<IGetCommentsQuery, EFGetCommentsQuery>();
            services.AddTransient<IFindCommentQuery, EFFindCommentQuery>();

            // ------------ Albums

            services.AddTransient<IGetAlbumsQuery, EFGetAlbumsQuery>();
            services.AddTransient<IFindAlbumQuery, EFFindAlbumQuery>();

        }

        public static void AddApplicationActorAndProvider(this IServiceCollection services)
        {

            //------------------------------- ACTOR

            services.AddTransient<IApplicationActorProvider>(x =>
            {

                var accessor = x.GetService<IHttpContextAccessor>();

                if (accessor == null || accessor.HttpContext == null)
                {
                    return new DefaultActorProvider();
                }

                var request = accessor.HttpContext.Request;

                var context = x.GetRequiredService<TuneTraxContext>();

                return new JwtActorProvider(request, context);
            });

            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();

                if (accessor == null || accessor.HttpContext == null)
                {
                    return new UnauthorizedActor();
                }


                return x.GetService<IApplicationActorProvider>().GetActor();
            });

        }



        public static IEnumerable<Claim> GetTokenClaims(this HttpRequest request)
        {
            var data = request.Headers.Authorization.ToString().Split("Bearer ");

            if (data.Length < 2)
            {
                throw new InvalidOperationException("You need to send token");
            }

            var token = data[1];

            var tokenJsonObject = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

            var claims = tokenJsonObject?.Claims;

            if (claims == null)
            {
                throw new InvalidOperationException("Wrong token format");
            }


            // if (ContainerExtensions.CheckIfTokenIsInInvalidList(claims))
            // {
            //     throw new InvalidOperationException("Token is invalid");
            // }


            return claims.ToList();

        }

        public static bool CheckIfTokenIsInInvalidList(IEnumerable<Claim> claims)
        {

            var storage = new ServiceCollection();
            storage.AddTransient<TuneTraxContext>();
            storage.AddTransient<DBTokenStorage>();
            var p2 = storage.BuildServiceProvider();
            var p3 = p2.GetService<DBTokenStorage>();

            if (p3 != null && p3.Exists(Guid.Parse(claims.First(x => x.Type == "jti").Value)))
            {
                return true;
            }
            return false;

        }
    }
}
