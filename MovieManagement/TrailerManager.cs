using Common.Lib;
using MovieManagement.Models;
using SQLHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement
{
    public class TrailerManager
    {
        public async Task<OperationResponse<string>> Add (TrailerViewModel model, string currentusername)
        {
            OperationResponse<string> response = new OperationResponse<string>();
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@TrailerLink", model.TrailerLink));
            param.Add(new KeyValue("@TrailerTitle", model.TrailerTitle));
            param.Add(new KeyValue("@CurrentUserName", currentusername));
            int opStatus = await sqlHelper.ExecuteNonQueryAsync("[dbo].[usp_trailer_add]", param, "@OpStatus");
            response.Result = model.TrailerID == 0 ? "Role Added Successfully" : "Role Updated Successfully";
            return response;
        }

        public async Task<List<TrailerItemModel>> index(int offset=0, int limit=20)
        {
            SQLHandlerAsync handlerAsync= new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@OffSet", offset));
            param.Add(new KeyValue("@Limit", limit));
            var trailerlist = await handlerAsync.ExecuteAsListAsync<TrailerItemModel>("[dbo].[usp_trailer_getAll]", param);
            return trailerlist;
        }

        public async Task<TrailerViewModel> GetTrailerByID(int TrailerID)
        {
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@TrailerID", TrailerID));
            var dbMoldel = await handlerAsync.ExecuteAsObjectAsync<TrailerViewModel>("[dbo].[usp_trailer_getByID]", param);
            return dbMoldel;
        }

        public async Task<OperationResponse<string>> deleteByID(int TrailerID)
        {
            OperationResponse<string> response = new OperationResponse<string>();
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@TrailerID", TrailerID));
            await handlerAsync.ExecuteNonQueryAsync("[dbo].[usp_trailer_deleteByID]", param);
            response.Result = "Trailer Deleted Successfully";
            return response;
        }
    }
}
