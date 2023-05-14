using Microsoft.AspNetCore.Mvc;
using WebApp.Base;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            ShowActionMessage("Hello action message", eMessageType.success);
            return View();
        }
    }
}
