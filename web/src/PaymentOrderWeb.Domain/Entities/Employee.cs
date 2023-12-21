using System.Text.Json.Serialization;

namespace PaymentOrderWeb.Domain.Entities
{
    public struct Employee
    {
        [JsonPropertyName("Codigo")]
        public int Code { get; set; }

        [JsonPropertyName("Nome")]
        public string Name { get; set; }

        [JsonPropertyName("TotalReceber")]
        public double TotalReceivable { get; set; }

        [JsonPropertyName("HorasExtras")]
        public double ExtraHours { get; set; }

        [JsonPropertyName("HorasDebito")]
        public double DebitHours { get; set; }

        [JsonPropertyName("DiasFalta")]
        public int MissingDays { get; set; }

        [JsonPropertyName("DiasExtras")]
        public int ExtraDays { get; set; }

        [JsonPropertyName("DiasTrabalhados")]
        public int WorkedDays { get; set; }

        [JsonIgnore]
        public double HourlyRate { get; set; }
    }   
}
