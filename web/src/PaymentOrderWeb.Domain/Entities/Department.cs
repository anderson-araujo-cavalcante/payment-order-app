namespace PaymentOrderWeb.Domain.Entities
{
    public class Department
    {
        public string Name { get; set; }
        public string ReferenceMonth { get; set; }
        public string ReferenceYear { get; set; }
        public decimal TotalToPay { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalExtra { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
