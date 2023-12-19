using CsvHelper.Configuration;
using PaymentOrderWeb.Domain.Entities;
using System.Globalization;

namespace PaymentOrderWeb.Application.CSVMap
{
    public sealed class EmployeeDataMap : ClassMap<EmployeeData>
    {
        public EmployeeDataMap()
        {
            string format = "dd/MM/yyyy";
            var msMY = CultureInfo.GetCultureInfo("pt-BR");

            Map(m => m.Code).Index(0).Name("Código", "Codigo");
            Map(m => m.Name).Index(1).Name("Nome");
            Map(m => m.HourlyRate).Index(2).Name("Valor hora").Convert(row => Math.Round(Convert.ToDecimal(row.Row.GetField<string>(2).Replace("R$ ", "")), 2));
            Map(m => m.Date).TypeConverterOption.Format(format).TypeConverterOption.CultureInfo(msMY).Index(3);
            Map(m => m.EntryTime).Index(4).Name("Entrada");
            Map(m => m.OutputTime).Index(5).Name("Saída", "Saida");
            Map(m => m.LunchTime).Index(6).Name("Almoço", "Almoco");
        }
    }
}
