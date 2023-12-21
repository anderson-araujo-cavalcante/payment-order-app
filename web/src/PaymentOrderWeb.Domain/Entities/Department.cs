using System.Text.Json.Serialization;

namespace PaymentOrderWeb.Domain.Entities
{
    public struct Department
    {
        [JsonPropertyName("Departamento")]
        public string Name { get; set; }

        [JsonPropertyName("MesVigencia")]
        public string ReferenceMonth { get; set; }

        [JsonPropertyName("AnoVigencia")]
        public string ReferenceYear { get; set; }

        [JsonPropertyName("TotalPagar")]
        public double TotalToPay { get; set; }

        [JsonPropertyName("TotalDesconto")]
        public double TotalDiscount { get; set; }

        [JsonPropertyName("TotalExtras")]
        public double TotalExtra { get; set; }

        [JsonPropertyName("Funcionarios")]
        public ICollection<Employee> Employees { get; set; }

        public Department() => Employees = new List<Employee>();
    }
}
