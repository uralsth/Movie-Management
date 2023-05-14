using Microsoft.AspNetCore.Mvc;
using MovieManagement;
using WebApp.Base;

namespace WebApp.Controllers
{
    public class MovieUserController : AdminBaseController
    {
        private readonly MovieManager _movieManager;
        public MovieUserController() { 
            _movieManager= new MovieManager();
        }
        public async Task<IActionResult> Details(int id)
        {
            var movieObject = await _movieManager.getMovieByID(id);

            var directorID = await _movieManager.getDirectorID(id);
            List<string> directorNames = new List<string>();
            foreach (var director in directorID)
            {
                directorNames.Add(director.PeopleName);
            }
            movieObject.MovieDirectors = directorNames.ToArray();

            var actorID = await _movieManager.getActorID(id);
            List<string> actorNames = new List<string>();
            foreach (var actor in actorID)
            {
                actorNames.Add(actor.PeopleName);
            }
            movieObject.MovieActors = actorNames.ToArray();

            var screenwriterID = await _movieManager.getScreenwriterID(id);
            List<string> screenwriterNames = new List<string>();
            foreach (var screenwriter in screenwriterID)
            {
                screenwriterNames.Add(screenwriter.PeopleName);
            }
            movieObject.MovieScreenwriters = screenwriterNames.ToArray();

            var platformID = await _movieManager.getPlatformID(id);
            List<string> platformNames = new List<string>();
            foreach (var platform in platformID)
            {
                platformNames.Add(platform.PlatformName);
            }
            movieObject.MoviePlatforms = platformNames.ToArray();

            var genreID = await _movieManager.getGenreID(id);
            List<string> genreNames = new List<string>();
            foreach (var genre in genreID)
            {
                genreNames.Add(genre.GenreName);
            }
            movieObject.MovieGenres = genreNames.ToArray();

            return View(movieObject);
        }
        public async Task<IActionResult> Index(int offset=0, int limit=20, string searchKeyword="")
        {
            ViewData["searchkeyword"] = searchKeyword;
            var movieList = await _movieManager.search(offset, limit, searchKeyword);
            foreach (var movie in movieList)
            {
                List<string> genrenames = new List<string>();
                var genreList = await new MovieManager().getGenreID(movie.MovieID);
                foreach (var genre in genreList)
                {
                    genrenames.Add(genre.GenreName);
                }
                movie.MovieGenres = genrenames.ToArray();

            }
            return View(movieList);
        }
    }
}
