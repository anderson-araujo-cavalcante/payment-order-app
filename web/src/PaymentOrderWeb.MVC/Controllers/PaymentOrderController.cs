using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PaymentOrderWeb.Application.Interfaces;
using PaymentOrderWeb.MVC.Models;
using System.Text;
using System.Text.Json;

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

            try
            {
                var result = await _paymentOrderAppService.ProcessAsync(multipleFile.Files);

                var json = System.Text.Json.JsonSerializer.Serialize(result, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                var content = new MemoryStream(bytes);

                return new FileStreamResult(content, new MediaTypeHeaderValue("application/json"))
                {
                    FileDownloadName = $"ordem-pagameto-{DateTime.Now.Date:dd/MM/yyyy}.json"
                };
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exceptions", ex.Message);
            }

            return View("Index", multipleFile);
        }
    }
}
