using Microsoft.AspNetCore.Mvc;

namespace PaymentOrderWeb.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
