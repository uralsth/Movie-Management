using Common.Lib;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieManagement;
using MovieManagement.Models;
using WebApp.Base;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class RoleController : AdminBaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly RoleManager _roleManager;

        public RoleController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _roleManager = new RoleManager();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["IsSuccess"] = "False";
            return View(new RoleViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                OperationResponse<string> rs = await _roleManager.AddUpdateRole(model, GetUserName);
                if (rs.IsSucess)
                {
                    ShowActionMessage(rs.Result, eMessageType.success);
                }
                else
                {
                    ShowActionMessage(string.Join(",", rs.ErrorMessage), eMessageType.danger);
                }
                return RedirectToAction(nameof(index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dbModel = await _roleManager.GetRoleByID(id);
            return View("create", dbModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            OperationResponse<string> rs = await _roleManager.deletebyID(id);
            if (rs.IsSucess)
            {
                ShowActionMessage(rs.Result, eMessageType.success);
            }
            else
            {
                ShowActionMessage(string.Join(",", rs.ErrorMessage), eMessageType.danger);
            }
            return RedirectToAction(nameof(index));

        }
        public async Task<IActionResult> index()
        {
            var rolelist = await _roleManager.GetRoleItems(0, 25);
            return View(rolelist);
        }
    }
}
