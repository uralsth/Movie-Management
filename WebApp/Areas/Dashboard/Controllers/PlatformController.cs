using Common.Lib;
using Microsoft.AspNetCore.Mvc;
using MovieManagement;
using MovieManagement.Models;
using WebApp.Base;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class PlatformController : AdminBaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly PlatformManager _platformManager;

        public PlatformController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _platformManager = new PlatformManager();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["IsSuccess"] = "False";
            return View(new PlatformViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlatformViewModel model)
        {
            if (ModelState.IsValid)
            {
                OperationResponse<string> rs = await _platformManager.addUpdate(model, GetUserName);
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
            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            var peopleList = await _platformManager.getAllPlatform(0, 20);
            return View(peopleList);
        }

        public async Task<IActionResult> Delete(int id)
        {
            OperationResponse<string> rs = await _platformManager.deleteByID(id);
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

        public async Task<IActionResult> Edit(int id)
        {
            var peopleObject = await _platformManager.getPlatformByID(id);
            return View("Create", peopleObject);
        }
    }
}
