using Common.Lib;
using MovieManagement.Models;
using SQLHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement
{
    public class MovieManager
    {
        public async Task<OperationResponse<string>> addUpdate(MovieViewModel model, string currentusername)
        {
            OperationResponse<string> response = new OperationResponse<string>();
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@MovieID", model.MovieID));
            param.Add(new KeyValue("@MovieName", model.MovieName));
            param.Add(new KeyValue("@ReleaseDate", model.ReleaseDate));
            param.Add(new KeyValue("@AddedBy", currentusername));
            param.Add(new KeyValue("@GenreIDs", string.Join(",", model.GenreIDs)));
            param.Add(new KeyValue("@ActorIDs", string.Join(",", model.ActorIDs)));
            param.Add(new KeyValue("@DirectorIDs", string.Join(",", model.DirectorIDs)));
            param.Add(new KeyValue("@ScreenwriterIDs", string.Join(",", model.ScreenwriterIDs)));
            param.Add(new KeyValue("@Runtime", string.Join(",", model.Runtime)));
            param.Add(new KeyValue("@PlatformIDs", string.Join(",", model.PlatformIDs)));
            param.Add(new KeyValue("@Language", model.Language));
            param.Add(new KeyValue("@PlotSummary", model.PlotSummary));
            param.Add(new KeyValue("@TrailerLink", model.TrailerLink));
            param.Add(new KeyValue("@ImagePath", model.ImagePath));
            int opStatus = await sqlHelper.ExecuteNonQueryAsync("[dbo].[usp_movie_addupdate]", param, "@OpStatus");
            response.Result = (model.MovieID == 0) ? "Movie Added Successfully" : "Movie Updated Successfully";
            return response;
        }

        public async Task<List<MovieItemModel>> getAllMovie(int offset, int limit)
        {
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@offset", offset));
            param.Add(new KeyValue("@Limit", limit));
            var movieList = await sqlHelper.ExecuteAsListAsync<MovieItemModel>("[dbo].[usp_movie_getAll]", param);
            return movieList;
        }

        public async Task<OperationResponse<string>> deleteByID(int MovieID)
        {
            OperationResponse<string> response= new OperationResponse<string>();
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@MovieID", MovieID));
            await sqlHelper.ExecuteNonQueryAsync("[dbo].[usp_movie_deleteByID]", param);
            response.Result = "Movie Deleted Successfully";
            return response;
        }

        public async Task<MovieViewModel> getMovieByIDForEdit(int MovieID)
        {
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@MovieID", MovieID));
            var movieObject = await handlerAsync.ExecuteAsObjectAsync<MovieViewModel>("[dbo].[usp_movie_getByID]", param);
            return movieObject;
        }
        public async Task<MovieItemModel> getMovieByID(int MovieID)
        {
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@MovieID", MovieID));
            var movieObject = await handlerAsync.ExecuteAsObjectAsync<MovieItemModel>("[dbo].[usp_movie_getByID]", param);
            return movieObject;
        }

        public async Task<List<MoviePeopleViewModel>> getActorID(int MovieID)
        {
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@MovieID", MovieID));
            var peopleList = await handlerAsync.ExecuteAsListAsync<MoviePeopleViewModel>("[dbo].[usp_MovieActorMap_getByMovieID]", param);
            return peopleList;
        }
        public async Task<List<MoviePeopleViewModel>> getDirectorID(int MovieID)
        {
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@MovieID", MovieID));
            var peopleList = await handlerAsync.ExecuteAsListAsync<MoviePeopleViewModel>("[dbo].[usp_MovieDirectorMap_getByMovieID]", param);
            return peopleList;
        }
        public async Task<List<MoviePeopleViewModel>> getScreenwriterID(int MovieID)
        {
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@MovieID", MovieID));
            var peopleList = await handlerAsync.ExecuteAsListAsync<MoviePeopleViewModel>("[dbo].[usp_MovieScreenwriterMap_getByMovieID]", param);
            return peopleList;
        }
        public async Task<List<MovieGenreViewModel>> getGenreID(int MovieID)
        {
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@MovieID", MovieID));
            var genreList = await handlerAsync.ExecuteAsListAsync<MovieGenreViewModel>("[dbo].[usp_MovieGenreMap_getByMovieID]", param);
            return genreList;
        }
        public async Task<List<MoviePlatformViewModel>> getPlatformID(int MovieID)
        {
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@MovieID", MovieID));
            var platformList = await handlerAsync.ExecuteAsListAsync<MoviePlatformViewModel>("[dbo].[usp_MoviePlatformMap_getByMovieID]", param);
            return platformList;
        }

        public async Task<List<MovieItemModel>> search(int offset, int limit, string searchkeyword)
        {
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@offset", offset));
            param.Add(new KeyValue("@Limit", limit));
            param.Add(new KeyValue("@SearchKeyword", searchkeyword));
            var searchList = await sqlHelper.ExecuteAsListAsync<MovieItemModel>("[dbo].[usp_movie_search]", param);
            return searchList;
        }
        public async Task<List<MovieItemModel>> getMovieByPlatform(int offset, int limit, string searchkeyword)
        {
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@SearchKeyword", searchkeyword));
            param.Add(new KeyValue("@OffSet", offset));
            param.Add(new KeyValue("@Limit", limit));
            var movieList = await sqlHelper.ExecuteAsListAsync<MovieItemModel>("[dbo].[usp_MoviePlatformMap_getMovieByPlatform]", param);
            return movieList;

        }
    }
}
