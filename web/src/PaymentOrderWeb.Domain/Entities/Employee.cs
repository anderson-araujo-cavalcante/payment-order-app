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
}
