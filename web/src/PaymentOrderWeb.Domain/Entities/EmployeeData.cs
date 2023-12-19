using CsvHelper.Configuration.Attributes;

namespace PaymentOrderWeb.Domain.Entities
{
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
    }
}
