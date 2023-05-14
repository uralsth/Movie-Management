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
    public class PlatformManager
    {
        public async Task<OperationResponse<string>> addUpdate(PlatformViewModel model, string currentusername)
        {
            OperationResponse<string> response = new OperationResponse<string>();
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@PlatformID", model.PlatformID));
            param.Add(new KeyValue("@PlatformName", model.PlatformName));
            param.Add(new KeyValue("@CurrentUserName", currentusername));
            int opStatus = await sqlHelper.ExecuteNonQueryAsync("[dbo].[usp_platform_addupdate]", param, "@OpStatus");
            response.Result = model.PlatformID == 0 ? "Platform Added Successfully" : "Platform Updated Successfully";
            return response;
        }

        public async Task<List<PlatformItemModel>> getAllPlatform(int offset, int limit)
        {
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@OffSet", offset));
            param.Add(new KeyValue("@Limit", limit));
            var platformList = await sqlHelper.ExecuteAsListAsync<PlatformItemModel>("[dbo].[usp_platform_getAll]", param);
            return platformList;
        }

        public async Task<OperationResponse<string>> deleteByID(int PlatformID)
        {
            OperationResponse<string> response = new OperationResponse<string>();
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@PlatformID", PlatformID));
            await handlerAsync.ExecuteNonQueryAsync("[dbo].[usp_platform_deleteByID]", param);
            response.Result = "Platform Deleted Successfully";
            return response;
        }

        public async Task<PlatformViewModel> getPlatformByID(int PlatformID)
        {
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@PlatformID", PlatformID));
            var platformObject = await handlerAsync.ExecuteAsObjectAsync<PlatformViewModel>("[dbo].[usp_platform_getByID]", param);
            return platformObject;
        }

    }
}
