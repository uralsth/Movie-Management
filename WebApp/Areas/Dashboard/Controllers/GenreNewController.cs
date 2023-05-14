using Common.Lib;
using Microsoft.AspNetCore.Mvc;
using MovieManagement;
using MovieManagement.Models;
using WebApp.Base;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class GenreNewController : AdminBaseController
    {

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly GenreManager _genreManager;

        public GenreNewController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _genreManager = new GenreManager();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData(int offset=0, int limit=20)
        {
            var genreList = await _genreManager.getAllGenre(offset, limit);
            return Json(genreList);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GenreViewModel model )
        {
            OperationResponse<string> rs = new OperationResponse<string>();
            if(ModelState.IsValid)
            {
                rs = await _genreManager.AddUpdateGenre(model, GetUserName);
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
            OperationResponse<string> rs = await _genreManager.deleteByID(id);
            return Json(rs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dbModel = await _genreManager.getGenreByID(id);
            return Json(dbModel);
        }
    }
}
