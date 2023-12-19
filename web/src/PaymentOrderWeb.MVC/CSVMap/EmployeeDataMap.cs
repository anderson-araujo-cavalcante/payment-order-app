using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using PaymentOrderWeb.Domain.Entities;
using System;
using System.Globalization;

namespace PaymentOrderWeb.MVC.CSVMap
{
    public sealed class EmployeeDataMap : ClassMap<EmployeeData>
    {
        public EmployeeDataMap()
        {
            string format = "dd/MM/yyyy";
            var msMY = CultureInfo.GetCultureInfo("pt-BR");

            Map(m => m.Code).Index(0).Name("Código", "Codigo");
            Map(m => m.Name).Index(1).Name("Nome");
            Map(m => m.HourlyRate).Index(2).Name("Valor hora").Convert(row => Math.Round(Convert.ToDecimal(row.Row.GetField<string>(2).Replace("R$ ","")), 2));
            //Map(m => m.Date).Index(3).Name("Data");
            Map(m => m.Date).TypeConverterOption.Format(format).TypeConverterOption.CultureInfo(msMY).Index(3);
            Map(m => m.EntryTime).Index(4).Name("Entrada");
            Map(m => m.OutputTime).Index(5).Name("Saída", "Saida");
            Map(m => m.LunchTime).Index(6).Name("Almoço", "Almoco");
            //Map(m => m.TotalHoraAlmoço).Index(6).Name("Almoço", "Almoco").Convert(row => Convert.ToInt16(row.Row.GetField<string>(2).Replace("R$ ", "")), 2)); ; //;
        }
    }

    public class EmployeeData
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
        //public DateTime Date { get; set; }

        [Format("dd/MM/yyyy")]
        public DateOnly Date { get; init; }

        //public string EntryTime { get; set; }
        [Format("HH:mm")]
        public TimeOnly EntryTime { get; init; }

        [Format("HH:mm")]
        public TimeOnly OutputTime { get; set; }
        public string LunchTime { get; set; }
        //public int TotalHoraAlmoço { get; set; }

        public double TotalHoraAlmoço()
        {
            //var tststs = new TimeOnly();
            var teste = LunchTime.Split(" - ");
            var fa = teste[0].Split(":");
            //var fa2 = teste[0].Split(":");


            //var fa4 = teste[1].Split(":");
            TimeOnly tctc;
            TimeOnly tctc2;
            TimeOnly.TryParse(teste[0], out tctc);
            TimeOnly.TryParse(teste[1], out tctc2);
            var taatata = tctc2 - tctc;
            var tttt = taatata.TotalMinutes;

            var tsts = Convert.ToInt16(fa[0]);
            var tsts1 = Convert.ToInt16(fa[1]);
            var time = new TimeSpan(tsts, tsts1, 0);


            var fa3 = teste[1].Split(":");
            var tsts2 = Convert.ToInt16(fa3[0]);
            var tsts3 = Convert.ToInt16(fa3[1]);
            var time2 = new TimeSpan(tsts2, tsts3, 0);


            var total = time2 - time;
            return total.TotalMinutes;
        }

        //public DateTime Data()
        //{
        //    var teste = Date.Split("/");
        //    var data = Convert.ToDateTime($"{teste[2]}-{teste[1]}-{teste[0]}");
        //    return data;
        //}
        //public void Validate()
        //{

        //}
    }
}
