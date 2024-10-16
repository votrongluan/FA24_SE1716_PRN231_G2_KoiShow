using Microsoft.AspNetCore.Mvc;

namespace KoiShow.MVCWebApp.Controllers
{
    public class RegisterFormController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
