using FluentAssertions;
using PaymentOrderWeb.Domain.Entities;
using PaymentOrderWeb.Domain.Interfaces.Services;
using PaymentOrderWeb.Domain.Services;

namespace PaymentOrderWeb.Domain.UnitTests
{
    public class PaymentOrderServiceTests
    {
        private readonly IPaymentOrderService _service;
        public PaymentOrderServiceTests()
        {
            _service = new PaymentOrderService();
        }

        [Fact]
        public async Task Should_ReturnSuccess()
        {
            /// Arrange
            var data = CreateEmployeeData();

            /// Act
            var result = await _service.Process1Async(data);

            /// Assert
            result.Should().NotBeNullOrEmpty();
            result.Single().Employees.Should().NotBeNullOrEmpty();
        }

        private static IDictionary<string, IEnumerable<EmployeeData>> CreateEmployeeData(int entryTime = 8, int outputTime = 18)
        {
            var employees = new Dictionary<string, IEnumerable<EmployeeData>>();
            var data = Enumerable.Empty<EmployeeData>();
            for (var i = 1; i <= 30; i++)
            {
                data = data.Append(new EmployeeData
                {
                    Code = 1,
                    Name = "João da Silva",
                    HourlyRate = 110.97,
                    Date = new DateOnly(2023, 04, i),
                    EntryTime = new TimeOnly(entryTime, 0),
                    OutputTime = new TimeOnly(outputTime, 0),
                    LunchTime = "12:00 - 13:00"
                });
            }
            employees.Add("test-abril-2023.csv", data);

            return employees;
        }

        private static IDictionary<string, IEnumerable<EmployeeData>> CreateEmployeeData2()
        {
            var employees = new Dictionary<string, IEnumerable<EmployeeData>>();
            var data = Enumerable.Empty<EmployeeData>();
            for (var i = 1; i <= 30; i++)
            {
                data = data.Append(new EmployeeData
                {
                    Code = 1,
                    Name = "João da Silva",
                    HourlyRate = 110.97,
                    Date = new DateOnly(2023, 04, i),
                    EntryTime = new TimeOnly(08, 0),
                    OutputTime = new TimeOnly(18, 0),
                    LunchTime = "12:00 - 13:00"
                });
            }
            employees.Add("test-abril-2023.csv", data);

            return employees;
        }
    }

}
