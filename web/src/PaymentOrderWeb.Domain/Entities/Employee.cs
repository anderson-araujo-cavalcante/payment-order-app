namespace PaymentOrderWeb.Domain.Entities
{
    public class Employee
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string TotalReceivable { get; set; }
        public string ExtraHours { get; set; }
        public string DebitHours { get; set; }
        public string MissingDays { get; set; }
        public string ExtraDays { get; set; }
        public string WorkedDays { get; set; }
    }

    public class EmployeeData
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string HourlyRate { get; set; }
        public DateTime Date { get; set; }


        public string EntryTime { get; set; }
        public string OutputTime { get; set; }
        public string LunchTime { get; set; }

        //public int TotalHoraAlmoço()
        //{
        //    var teste = LunchTime.Split(" - ");
        //    return (int)teste[0] -(int) teste[1];
        //}

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
