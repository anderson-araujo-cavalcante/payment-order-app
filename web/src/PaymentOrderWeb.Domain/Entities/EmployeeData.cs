using CsvHelper.Configuration.Attributes;
using System.Runtime.CompilerServices;

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
       // [Format("dd/MM/yyyy")]
        public DateOnly Date { get; init; }
        //[Format("HH:mm")]
        public TimeOnly EntryTime { get; init; }
       // [Format("HH:mm")]
        public TimeOnly OutputTime { get; set; }
    }
}
