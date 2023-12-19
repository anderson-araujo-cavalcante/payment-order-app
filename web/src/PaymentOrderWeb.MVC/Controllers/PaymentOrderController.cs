using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using PaymentOrderWeb.Application.Interfaces;
using PaymentOrderWeb.Domain.Entities;
using PaymentOrderWeb.MVC.CSVMap;
using PaymentOrderWeb.MVC.Models;
using System.Text;

namespace PaymentOrderWeb.MVC.Controllers
{
    public class PaymentOrderController : Controller
    {
        private readonly IPaymentOrderAppService _paymentOrderAppService;

        public PaymentOrderController(IPaymentOrderAppService paymentOrderAppService)
        {
                _paymentOrderAppService = paymentOrderAppService ?? throw new ArgumentNullException(nameof(paymentOrderAppService));
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadFiles(MultipleFileViewModel multipleFile)
        {
            var list = new List<CSVMap.EmployeeData>();

            foreach (var file in multipleFile.Files)
            {
                using var memoryStream = new MemoryStream(new byte[file.Length]);
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null,
                    HasHeaderRecord = false,
                    Delimiter = ";",
                    Encoding = Encoding.UTF8
                };

                using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                using (var csvReader = new CsvReader(reader, csvConfig))
                {
                    csvReader.Context.RegisterClassMap<EmployeeDataMap>();

                    csvReader.Read();
                    var records = csvReader.GetRecords<CSVMap.EmployeeData>();
                    list.AddRange(records);
                }
            }
            //_paymentOrderAppService.Process(list);

            return View("Index");
        }
    }
}
