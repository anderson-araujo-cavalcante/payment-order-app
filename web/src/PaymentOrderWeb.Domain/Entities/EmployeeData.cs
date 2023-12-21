namespace PaymentOrderWeb.Domain.Entities
{
    public struct EmployeeData
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public double HourlyRate { get; set; }
        public string LunchTime { get; set; }
        public double TotalReceivable { get; set; }
        public double ExtraHours { get; set; }
        public double DebitHours { get; set; }
        public int MissingDays { get; set; }
        public int ExtraDays { get; set; }
        public int WorkedDays { get; set; }
        public DateOnly Date { get; init; }
        public TimeOnly EntryTime { get; init; }
        public TimeOnly OutputTime { get; set; }
    }
}
