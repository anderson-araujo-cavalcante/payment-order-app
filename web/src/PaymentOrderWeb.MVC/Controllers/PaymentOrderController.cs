using Microsoft.AspNetCore.Mvc;
using PaymentOrderWeb.Application.Interfaces;
using PaymentOrderWeb.MVC.Models;

namespace PaymentOrderWeb.MVC.Controllers
{
    public class PaymentOrderController(IPaymentOrderAppService paymentOrderAppService) : Controller
    {
        private readonly IPaymentOrderAppService _paymentOrderAppService = paymentOrderAppService ?? throw new ArgumentNullException(nameof(paymentOrderAppService));

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadFiles(MultipleFileViewModel multipleFile)
        {
            if (multipleFile is null) throw new ArgumentNullException(nameof(multipleFile));

            await _paymentOrderAppService.ProcessAsync(multipleFile.Files);

            return View("Index");
        }
    }
}
