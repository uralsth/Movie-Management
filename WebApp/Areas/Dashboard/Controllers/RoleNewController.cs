using Common.Lib;
using Microsoft.AspNetCore.Mvc;
using MovieManagement;
using MovieManagement.Models;
using WebApp.Base;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class RoleNewController : AdminBaseController
    {
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        private readonly RoleManager _roleManager;

        public RoleNewController(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            _roleManager = new RoleManager();
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData(int offset = 0, int limit = 20)
        {
            //searchKeyword = searchKeyword ?? string.Empty;
            var roleList = await _roleManager.GetRoleItems(offset, limit);
            return Json(roleList);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleViewModel model )
        {
            OperationResponse<string> rs = new OperationResponse<string>();
            if(ModelState.IsValid)
            {
                rs = await _roleManager.AddUpdateRole(model, GetUserName);
                if (rs.IsSucess)
                {
                    ShowActionMessage(rs.Result, eMessageType.success);
                }
                else
                {
                    ShowActionMessage(string.Join(",",rs.ErrorMessage), eMessageType.error);
                }
            }
            else
            {
                rs.AddError(GetModelStateError());
            }
            return Json(rs);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            OperationResponse<string> rs = await _roleManager.deletebyID(id);
            return Json(rs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dbModel = await _roleManager.GetRoleByID(id);
            return Json(dbModel);
        }
    }
}
