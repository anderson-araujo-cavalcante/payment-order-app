using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using PaymentOrderWeb.Application.CSVMap;
using PaymentOrderWeb.Application.Exceptions;
using PaymentOrderWeb.Application.Interfaces;
using PaymentOrderWeb.Domain.Entities;
using PaymentOrderWeb.Domain.Interfaces.Services;
using PaymentOrderWeb.Infrasctructure.Extensions;
using System.Collections.Concurrent;
using System.Globalization;
using System.Text;

namespace PaymentOrderWeb.Application.Concrets
{
    public class PaymentOrderAppService : IPaymentOrderAppService
    {
        private readonly IPaymentOrderService _paymentOrderService;

        public PaymentOrderAppService(IPaymentOrderService paymentOrderService)
        {
            _paymentOrderService = paymentOrderService ?? throw new ArgumentNullException(nameof(paymentOrderService));
        }

        public async Task<IEnumerable<Department>> ProcessAsync(IEnumerable<IFormFile> files)
        {
            if (files is null) throw new ArgumentNullException(nameof(files));

            var list = new Dictionary<string, IEnumerable<EmployeeData>>();

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                HasHeaderRecord = false,
                Delimiter = ";",
                Encoding = Encoding.UTF8
            };

            if (SynchronizationContext.Current == null)
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
           
            var exceptions = new ConcurrentQueue<Exception>();            

            await files.AsyncParallelForEach(async file =>
            {
                using var memoryStream = new MemoryStream(new byte[file.Length]);
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                using var reader = new StreamReader(memoryStream, Encoding.UTF8);
                using var csvReader = new CsvReader(reader, csvConfig);
                csvReader.Context.RegisterClassMap<EmployeeDataMap>();

                csvReader.Read();
                try
                {
                    var records = csvReader.GetRecords<EmployeeData>();

                    if (list.TryGetValue(file.FileName, out var value))
                    {
                        list[file.FileName] = value.Concat(records.ToList());
                    }
                    else
                    {
                        list.Add(file.FileName, records.ToList());
                    }
                }
                catch (Exception) 
                {
                    exceptions.Enqueue(new InconsistentSpreadsheetException(file.FileName));
                }

            }, 20, TaskScheduler.FromCurrentSynchronizationContext());

            if (!exceptions.IsEmpty)
            {
                throw new AggregateException(exceptions);
            }

            return await _paymentOrderService.ProcessAsync(list);
        }
    }
}
