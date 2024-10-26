using Microsoft.AspNetCore.Mvc;

namespace KoiShow.MVCWebApp.Controllers
{
    public class PaymentsAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
