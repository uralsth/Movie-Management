using Microsoft.AspNetCore.Mvc;
using MovieManagement;

namespace WebApp.Views.Shared.Components.Movie
{
    public class MovieViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int Limit, string platformname)
        {
            var nowShowingMovieList = await new MovieManager().getMovieByPlatform(0, Limit, platformname);
            foreach (var movie in nowShowingMovieList)
            {
                List<string> genrenames = new List<string>();
                var genreList = await new MovieManager().getGenreID(movie.MovieID);
                foreach (var genre in genreList)
                {
                    genrenames.Add(genre.GenreName);
                }
                movie.MovieGenres = genrenames.ToArray();

            }
            return View("NowShowingMovie",nowShowingMovieList);
        }
    }
}
