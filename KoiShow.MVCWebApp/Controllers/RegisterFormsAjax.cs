using Microsoft.AspNetCore.Mvc;

namespace KoiShow.MVCWebApp.Controllers
{
    public class RegisterFormsAjax : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
