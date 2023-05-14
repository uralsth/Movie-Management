using Common.Lib;
using Microsoft.AspNetCore.Mvc;
using MovieManagement;
using MovieManagement.Models;
using WebApp.Base;
using WebApp.Models;
using System;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class MovieController : AdminBaseController
    {

        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        private readonly MovieManager _movieManager;
        private readonly PeopleManager _peopleManager;
        private readonly PlatformManager _platformManager;
        private readonly GenreManager _genreManager;
        public MovieController(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostEnvironment)
        {
            _hostingEnvironment = hostEnvironment;
            _movieManager = new MovieManager();
            _peopleManager = new PeopleManager();
            _platformManager = new PlatformManager();
            _genreManager = new GenreManager();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadGenreList();
            await LoadActorList();
            await LoadDirectorList();
            await LoadScreenwriterList();
            await LoadPlatformList();
            ViewData["IsSuccess"] = "False";
            return View(new MovieViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImagePath == null && model.FormFile == null)
                {
                    await LoadGenreList();
                    await LoadActorList();
                    await LoadDirectorList();
                    await LoadScreenwriterList();
                    await LoadPlatformList();
                    ShowActionMessage("Please choose poster", eMessageType.danger);
                    return View(model);
                }
                else if (model.FormFile != null)// if file present then save file
                {

                    // save file here.                   

                    string[] allowedImageTypes = new string[] { "image/jpeg", "image/png" };
                    if (!allowedImageTypes.Contains(model.FormFile.ContentType.ToLower()))
                    {
                        ShowActionMessage("Please upload image file", eMessageType.danger);
                        return View(model);
                    }
                    string savePath = Path.Combine(_hostingEnvironment.WebRootPath, "movieImage");
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
                    // set save image path here.
                    model.ImagePath = "/movieimage/" + fileName;
                }
                OperationResponse<string> rs = await _movieManager.addUpdate(model, GetUserName);
                if (rs.IsSucess)
                {
                    ShowActionMessage(rs.Result, eMessageType.success);
                }
                else
                {
                    ShowActionMessage(string.Join(",", rs.ErrorMessage), eMessageType.danger);
                }
                return RedirectToAction(nameof(Index));
            }
            await LoadGenreList();
            await LoadActorList();
            await LoadDirectorList();
            await LoadScreenwriterList();
            await LoadPlatformList();
            return View(model);
        }

        public async Task<IActionResult> Index(int offset=0, int limit=20, string searchkeyword="")
        {
            ViewData["searchkeyword"] = searchkeyword;
            var movieList = await _movieManager.search(offset,limit, searchkeyword);
            return View(movieList);
        }

        private async Task LoadActorList()
        {
            var actorList = await _peopleManager.GetPeopleByRoles(0, 20,"Actor");
            ViewBag.ActorList = actorList;
        }
        private async Task LoadDirectorList()
        {
            var directorList = await _peopleManager.GetPeopleByRoles(0, 20,"Director");
            ViewBag.DirectorList = directorList;
        }
        private async Task LoadScreenwriterList()
        {
            var screenwriterList = await _peopleManager.GetPeopleByRoles(0, 20,"Screenwriter");
            ViewBag.ScreenwriterList = screenwriterList;
        }

        private async Task LoadPlatformList()
        {
            var platformList = await _platformManager.getAllPlatform(0, 20);
            ViewBag.PlatformList = platformList;
        }

        private async Task LoadGenreList()
        {
            var genreList = await _genreManager.getAllGenre(0, 20);
            ViewBag.GenreList = genreList;
        }


        public async Task<IActionResult> Edit(int id)
        {
            await LoadGenreList();
            await LoadActorList();
            await LoadDirectorList();
            await LoadScreenwriterList();
            await LoadPlatformList();
            var movieModel = await _movieManager.getMovieByIDForEdit(id);

            var actorList = await _movieManager.getActorID(id);
            List<int> actorids = new List<int>();
            foreach (var actor in actorList)
            {
                actorids.Add(actor.PeopleID);
            }
            movieModel.ActorIDs = actorids.ToArray();


            var directorList = await _movieManager.getDirectorID(id);
            List<int> directorids = new List<int>();
            foreach (var director in directorList)
            {
                directorids.Add(director.PeopleID);
            }
            movieModel.DirectorIDs = directorids.ToArray();

            var screenwriterList = await _movieManager.getScreenwriterID(id);
            List<int> screenwriterids = new List<int>();
            foreach (var screenwriter in screenwriterList)
            {
                screenwriterids.Add(screenwriter.PeopleID);
            }
            movieModel.ScreenwriterIDs = screenwriterids.ToArray();

            var genreList = await _movieManager.getGenreID(id);
            List<int> genreids = new List<int>();
            foreach (var genre in genreList)
            {
                genreids.Add(genre.GenreID);
            }
            movieModel.GenreIDs = genreids.ToArray();

            var platformList = await _movieManager.getPlatformID(id);
            List<int> platformids = new List<int>();
            foreach (var platform in platformList)
            {
                platformids.Add(platform.PlatformID);
            }
            movieModel.PlatformIDs = platformids.ToArray();
            return View("Create", movieModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            OperationResponse<string> rs = await _movieManager.deleteByID(id);
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
