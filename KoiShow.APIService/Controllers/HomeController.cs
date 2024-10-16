using Microsoft.AspNetCore.Mvc;

namespace KoiShow.APIService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
