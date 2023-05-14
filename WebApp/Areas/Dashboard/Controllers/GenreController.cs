using Common.Lib;
using Microsoft.AspNetCore.Mvc;
using MovieManagement;
using MovieManagement.Models;
using SQLHelper;
using System.Runtime.InteropServices;
using WebApp.Base;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class GenreController : AdminBaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly GenreManager _genreManager;

        public GenreController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _genreManager = new GenreManager();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["IsSuccess"] = "False";
            return View(new GenreViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreViewModel model)
        {
            if (ModelState.IsValid)
            {
                OperationResponse<string> rs = await _genreManager.AddUpdateGenre(model, GetUserName);
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

        public async Task<IActionResult> index()
        {
            var genrelist = await _genreManager.getAllGenre(0, 20);
            return View(genrelist);
        }

        public async Task<IActionResult> Delete(int id)
        {
            OperationResponse<string> rs = await _genreManager.deleteByID(id);
            if (rs.IsSucess)
            {
                ShowActionMessage(rs.Result,eMessageType.success);
            }
            else
            {
                ShowActionMessage(string.Join(",", rs.ErrorMessage),eMessageType.danger);
            }
            return RedirectToAction(nameof(index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dbModel = await _genreManager.getGenreByID(id);
            return View("Create", dbModel);
        }
    }
}
