using DataAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialDataController : ControllerBase
    {
        // GET: api/<InitialDataController>

        [HttpGet]
        public IActionResult Get()
        {
            var context = new TuneTraxContext();

            if (context.Tracks.Any())
            {
                return Conflict();
            }


            Genre g1 = new() { Name = "Rock" };
            Genre g2 = new() { Name = "HipHop" };
            Genre g3 = new() { Name = "Pop" };
            Genre g4 = new() { Name = "Jazz" };
            Genre g5 = new() { Name = "Reggae" };
            Genre g6 = new() { Name = "Funk" };

            List<Genre> genres1 = new List<Genre>() {g1, g2 };
            List<Genre> genres2 = new List<Genre>() {g1, g2, g3 };
            List<Genre> genres3 = new List<Genre>() {g2, g3, g5 };
            List<Genre> genres4 = new List<Genre>() {g1, g5, g6 };
            List<Genre> genres5 = new List<Genre>() {g4, g5};

            Country c1 = new() { Name= "Finland" };
            Country c2 = new() { Name = "Egypt" };
            Country c3 = new() { Name = "Peru" };

            //Role r1 = new() { Name = "admin" };
            //Role r2 = new() { Name = "user" };

            Artist artist1 = new() { Name = "Brzi123", FirstName = "Brzi", LastName = "Gonzales", DateOfBirth = new DateOnly(2000, 10, 10), Country = c1, Gender = Gender.Male, Genres = genres1 };
            Artist artist2 = new() { Name = "Miki511", FirstName = "Miki", LastName = "Maus", DateOfBirth = new DateOnly(2001, 10, 10), Country = c2, Gender = Gender.Male, Genres = genres4 };
            Artist artist3 = new() { Name = "pinky2", FirstName = "Pink", LastName = "Panter", DateOfBirth = new DateOnly(2001, 11, 11), Country = c3, Gender = Gender.Male ,Genres = genres2 };

            Album album1 = new () { Name = "Album A", Artist = artist1, ReleaseDate = new DateTime(2019,5,5)};
            Album album2 = new () { Name = "Album B", Artist = artist2, ReleaseDate = new DateTime(2022,7,12)};

            User user1 = new() { Country = c2, Username = "admin", Email = "admin123A", DateOfBirth = new DateOnly(2019, 5, 5), Gender = Gender.Male, Password =  "password123"};

            Track track1 = new() { Album = album1, Duration = 200, Title = "Song1", AudioFile = "default.mp3", ReleaseDate = new DateTime(2020,10,10), Genres = genres2, Artists = new List<Artist> { album1.Artist } };
            Track track2 = new() { Album = album2, Duration = 300, Title = "Song2", AudioFile = "default.mp3", ReleaseDate = new DateTime(2023,10,10), Genres = new List<Genre>() { g2, g4} , Artists = new List<Artist> { album2.Artist } };
            Track track3= new() { Duration = 240, Title = "Song3", AudioFile = "default.mp3", ReleaseDate = new DateTime(2022,1,1), Artists = new List<Artist>() { artist3, artist2 }, Genres = new List<Genre>() { g3} };


            context.Tracks.Add(track1);
            context.Tracks.Add(track2);
            context.Tracks.Add(track3);

            context.Users.Add(user1);

            context.SaveChanges();

            return NoContent();
        }
    }
}
