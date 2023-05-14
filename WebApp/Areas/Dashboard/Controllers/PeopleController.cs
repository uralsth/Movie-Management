using Common.Lib;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MovieManagement;
using MovieManagement.Models;
using SQLHelper;
using System;
using System.Reflection;
using WebApp.Base;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class PeopleController : AdminBaseController
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly PeopleManager _peopleManager;

        public PeopleController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _peopleManager = new PeopleManager();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadRoleList();
            ViewData["IsSuccess"] = "False";
            return View(new PeopleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PeopleViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.ImagePath == null && model.FormFile == null)
                {
                    await LoadRoleList();
                    ShowActionMessage("Please Choose product image", eMessageType.danger);
                    return View(model);
                }
                else if (model.FormFile != null)
                {
                    string[] allowedImageTypes = new string[] { "image/jpeg", "image/png" };
                    if (!allowedImageTypes.Contains(model.FormFile.ContentType.ToLower()))
                    {
                        ShowActionMessage("Please upload image file", eMessageType.danger);
                        return View(model);
                    }
                    string savePath = Path.Combine(_hostingEnvironment.WebRootPath, "moviepeopleimage");
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    string fileName = model.FormFile.FileName;
                    savePath = Path.Combine(savePath, fileName);

                    using (FileStream fileStream = new FileStream(savePath, FileMode.Create))
                    {
                        model.FormFile.CopyTo(fileStream);
                    }
                    model.ImagePath = "/moviepeopleimage/" + fileName;
                }
                OperationResponse<string> rs = await _peopleManager.addUpdate(model, GetUserName);
                if (rs.IsSucess)
                {
                    ShowActionMessage(rs.Result, eMessageType.success);
                }
                else
                {
                    ShowActionMessage(string.Join(",", rs.Result), eMessageType.danger);
                }
                return RedirectToAction(nameof(Index));
            }
            await LoadRoleList();
            return View(model);
        }

        private async Task LoadRoleList()
        {
            var rolelist = await new RoleManager().GetRoleItems(0, 25);
            ViewBag.RoleList = rolelist;
        }

        public async Task<IActionResult> Index()
        {
            var peopleList = await _peopleManager.GetALLPeople(0, 20);
            return View(peopleList);
        }

        public async Task<IActionResult> Edit(int id)
        {
            await LoadRoleList();
            var peopleList = await _peopleManager.GetPeopleByID(id);
            var roleList = await _peopleManager.getRoleID(id);
            List<int> roleids = new List<int>();
            foreach (var role in roleList)
            {
                roleids.Add(role.RoleID);
            }
            peopleList.RoleIDs = roleids.ToArray();
            return View("Create", peopleList);
        }

        public async Task<IActionResult> Delete(int id)
        {
            OperationResponse<string> rs = await _peopleManager.DeleteByID(id);
            if (rs.IsSucess)
            {
                ShowActionMessage(rs.Result, eMessageType.success);
            }
            else
            {
                ShowActionMessage(string.Join(",", rs.Result), eMessageType.danger);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
