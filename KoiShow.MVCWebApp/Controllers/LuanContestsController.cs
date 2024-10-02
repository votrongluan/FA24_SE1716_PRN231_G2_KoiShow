using Microsoft.AspNetCore.Mvc;

namespace KoiShow.MVCWebApp.Controllers
{
    public class LuanContestsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
