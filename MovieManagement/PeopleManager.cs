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
    public class PeopleManager
    {
        public async Task<OperationResponse<string>> addUpdate(PeopleViewModel model, string currentusername)
        {
                OperationResponse<string> response = new OperationResponse<string>();
                SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
                IList<KeyValue> param = new List<KeyValue>();
                param.Add(new KeyValue("@PeopleID", model.PeopleID));
                param.Add(new KeyValue("@PeopleName", model.PeopleName));
                param.Add(new KeyValue("@Gender", model.Gender));
                param.Add(new KeyValue("@CurrentUserName", currentusername));
                param.Add(new KeyValue("@ImagePath", model.ImagePath));
                param.Add(new KeyValue("@RoleIDs", string.Join(",",model.RoleIDs)));
                param.Add(new KeyValue("@DateOfBirth", model.DateOfBirth));
                int opStatus = await sqlHelper.ExecuteNonQueryAsync("[dbo].[usp_people_addupdate]", param, "@OpStatus");
                response.Result = model.PeopleID == 0 ? "People Addded Successfully" : "People Updated Successfully.";
                return response;
        }

        public async Task<List<PeopleItemModel>> GetPeopleByRoles(int offset, int limit, string SearchKeyword)
        {
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@OffSet", offset));
            param.Add(new KeyValue("@Limit", limit));
            param.Add(new KeyValue("@SearchKeyword", SearchKeyword));
            var peopleList = await sqlHelper.ExecuteAsListAsync<PeopleItemModel>("[dbo].[usp_people_getByRole]", param);
            return peopleList;

        }
        public async Task<List<PeopleItemModel>> GetALLPeople(int offset, int limit)
        {
            SQLHandlerAsync sqlHelper = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@OffSet", offset));
            param.Add(new KeyValue("@Limit", limit));
            var peopleList = await sqlHelper.ExecuteAsListAsync<PeopleItemModel>("[dbo].[usp_people_getAll]", param);
            return peopleList;

        }

        public async Task<PeopleViewModel> GetPeopleByID(int PeopleID)
        {
            SQLHandlerAsync handlerAsync= new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@PeopleID", PeopleID));
            var peopleObject = await handlerAsync.ExecuteAsObjectAsync<PeopleViewModel>("[dbo].[usp_people_getByID]", param);
            return peopleObject;
        }

        public async Task<OperationResponse<string>> DeleteByID(int PeopleID)
        {
            OperationResponse<string> response = new OperationResponse<string>();
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@PeopleID", PeopleID));
            await handlerAsync.ExecuteNonQueryAsync("[dbo].[usp_people_deleteByID]", param);
            response.Result = "People Deleted Successfully";
            return response;
        }

        public async Task<List<PeopleRoleViewModel>> getRoleID(int PeopleID)
        {
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>();
            param.Add(new KeyValue("@PeopleID", PeopleID));
            var roleList = await handlerAsync.ExecuteAsListAsync<PeopleRoleViewModel>("[dbo].[usp_PeopleRoleMap_getbypeopleid]", param);
            return roleList;
        }
    }
}
