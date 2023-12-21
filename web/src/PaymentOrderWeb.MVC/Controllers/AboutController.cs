using Microsoft.AspNetCore.Mvc;

namespace PaymentOrderWeb.MVC.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
