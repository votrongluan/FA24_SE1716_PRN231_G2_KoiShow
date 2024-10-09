using Microsoft.AspNetCore.Mvc;

namespace KoiShow.MVCWebApp.Controllers
{
    public class ContestsAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
