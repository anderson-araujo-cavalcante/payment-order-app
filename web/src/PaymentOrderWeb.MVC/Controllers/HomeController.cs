using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using PaymentOrderWeb.Domain.Entities;
using PaymentOrderWeb.MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Formats.Asn1;
using System.IO;
using System.Text;

namespace PaymentOrderWeb.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[HttpPost]
        //public async Task<ActionResult> UploadFiles(MultipleFileViewModel multipleFile)
        //{
        //    var list = new List<EmployeeData>();

        //    foreach (var file in multipleFile.Files)
        //    {
        //        using var memoryStream = new MemoryStream(new byte[file.Length]);
        //        await file.CopyToAsync(memoryStream);
        //        memoryStream.Position = 0;

        //        var csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
        //        {
        //            HeaderValidated = null,
        //            MissingFieldFound = null,
        //            HasHeaderRecord = false,
        //            Delimiter = ";",
        //            Encoding = Encoding.UTF8
        //        };

        //        using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
        //        using (var csvReader = new CsvReader(reader, csvConfig))
        //        {
        //             csvReader.Context.RegisterClassMap<EmployeeDataMap>();

        //            csvReader.Read();
        //            var records = csvReader.GetRecords<EmployeeData>();
        //            list.AddRange(records);
        //        }
        //    }           

        //    return View("Index");
        //}
    }
}
