using Common.Lib;
using MovieManagement.Models;
using SQLHelper;

namespace MovieManagement
{
    public class GenreManager
    {
        public async Task<OperationResponse<string>> AddUpdateGenre(GenreViewModel model, string currentusername)
        {
            OperationResponse<string> response = new OperationResponse<string>();
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@GenreID", model.GenreID));
            param.Add(new KeyValue("@GenreName",model.GenreName));
            param.Add(new KeyValue("@Description", model.Description));
            param.Add(new KeyValue("@CurrentUserName", currentusername));
            int opStatus = await sqlHelper.ExecuteNonQueryAsync("[dbo].[usp_genre_addupdate]", param, "@OpStatus");
            response.Result = model.GenreID == 0 ? "Genre Added Successfully" : "Genre Updated Successfully";
            return response;
        }

        public async Task<List<GenreItemModel>> getAllGenre(int offset, int limit)
        {
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@offset", offset));
            param.Add(new KeyValue("@Limit", limit));
            var genrelist = await sqlHelper.ExecuteAsListAsync<GenreItemModel>("[dbo].[usp_genre_getAll]", param);
            return genrelist;
        }

        public async Task<OperationResponse<string>> deleteByID(int GenreID)
        {
            OperationResponse<string> response = new OperationResponse<string>();
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@GenreID", GenreID));
            await handlerAsync.ExecuteNonQueryAsync("[dbo].[usp_genre_deleteByID]", param);
            response.Result = "Genre Deleted Succesfully";
            return response;
        }

        public async Task<GenreViewModel> getGenreByID(int GenreID)
        {
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@GenreID", GenreID));
            var dbModel = await sqlHelper.ExecuteAsObjectAsync<GenreViewModel>("[dbo].[usp_genre_getByID]", param);
            return dbModel;

        }

    }
}
