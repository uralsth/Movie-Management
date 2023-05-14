using Common.Lib;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MovieManagement;
using MovieManagement.Models;
using WebApp.Base;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class PeopleNewController : AdminBaseController
    {

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly PeopleManager _peopleManager;

        public PeopleNewController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _peopleManager = new PeopleManager();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await LoadRoleList();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData(int offset=0, int limit=20, string searchKeyword="")
        {
            searchKeyword = searchKeyword ?? string.Empty;
            var thelist = await _peopleManager.GetPeopleByRoles(offset, limit, searchKeyword);
            return Json(thelist);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PeopleViewModel model)
        {
            OperationResponse<string> rs = new OperationResponse<string>();
            if (ModelState.IsValid)
            {
                rs = await _peopleManager.addUpdate(model, GetUserName);
                if (rs.IsSucess)
                {
                    ShowActionMessage(rs.Result, WebApp.Models.eMessageType.success);
                }
                else
                {
                    ShowActionMessage(string.Join(",", rs.ErrorMessage), eMessageType.danger);
                }
            }
            else
            {
                rs.AddError(GetModelStateError());
            }
            await LoadRoleList();
            return Json(rs);
        }

        private async Task LoadRoleList()
        {
            var rolelist = await new RoleManager().GetRoleItems(0, 25);
            ViewBag.NewRoleList = rolelist;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await LoadRoleList();
            var dbModel = await _peopleManager.GetPeopleByID(id);
            var roleList = await _peopleManager.getRoleID(id);

            List<int> roleids = new List<int>();
            foreach (var role in roleList)
            {
                roleids.Add(role.RoleID);
            }
            dbModel.RoleIDs = roleids.ToArray();
            return Json(dbModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            OperationResponse<string> rs = await _peopleManager.DeleteByID(id);
            return Json(rs);
        }


    }
}
