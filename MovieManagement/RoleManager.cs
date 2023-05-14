using Common.Lib;
using MovieManagement.Models;
using SQLHelper;

namespace MovieManagement
{
    public class RoleManager
    {
        public async Task<OperationResponse<string>> AddUpdateRole(RoleViewModel model, string currentusername)
        {
            OperationResponse<string> response = new OperationResponse<string>();
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@RoleID", model.RoleID));
            param.Add(new KeyValue("@RoleName", model.RoleName));
            param.Add(new KeyValue("@Description", model.Description));
            param.Add(new KeyValue("@CurrentUserName", currentusername));

            int opStatus = await sqlHelper.ExecuteNonQueryAsync("[dbo].[usp_role_addupdate]", param, "@OpStatus");

            response.Result = model.RoleID == 0 ? "Role Added Successfully" : "Role Updated Successfully";
            return response;
        }

        public async Task<List<RoleItemModel>> GetRoleItems(int offset, int limit)
        {
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@offset", offset));
            param.Add(new KeyValue("@Limit", limit));
            var rolelist = await sqlHelper.ExecuteAsListAsync<RoleItemModel>("[dbo].[usp_role_getAll]", param);
            return rolelist;

        }

        public async Task<List<RoleItemModel>> search(int offset, int limit, string searchkeyword)
        {
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@offset", offset));
            param.Add(new KeyValue("@Limit", limit));
            param.Add(new KeyValue("@searchKeyword", searchkeyword));
            var rolelist = await sqlHelper.ExecuteAsListAsync<RoleItemModel>("[dbo].[usp_role_search]", param);
            return rolelist;

        }
        public async Task<RoleViewModel> GetRoleByID(int RoleID)
        {
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@RoleID", RoleID));
            var dbModel = await handlerAsync.ExecuteAsObjectAsync<RoleViewModel>("[dbo].[usp_role_getByID]", param);
            return dbModel;
        }

        public async Task<OperationResponse<string>> deletebyID(int RoleID)
        {
            OperationResponse<string> response = new OperationResponse<string>();
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@RoleID", RoleID));
            await handlerAsync.ExecuteNonQueryAsync("[dbo].[usp_role_deleteByID]", param);
            response.Result = "Role Deleted Successfully";
            return response;
        }
    }
}