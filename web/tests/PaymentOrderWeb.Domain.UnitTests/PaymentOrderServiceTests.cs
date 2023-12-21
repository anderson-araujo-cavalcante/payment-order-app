using FluentAssertions;
using PaymentOrderWeb.Domain.Entities;
using PaymentOrderWeb.Domain.Interfaces.Services;
using PaymentOrderWeb.Domain.Services;
using PaymentOrderWeb.Infrasctructure.Extensions;

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
            var result = await _service.ProcessAsync(data);

            /// Assert
            result.Should().NotBeNullOrEmpty();
            result.Single().Employees.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task DaysNotWorkedShouldBeDeducted()
        {
            /// Arrange
            var dailyWorkload = 8;
            var discountDays = 2;
            var discountCalendarDays = 4;
            var hourlyRate = 110.97;
            var totalValueDiscountInDepartment = hourlyRate * dailyWorkload * discountDays;
            var data = CreateEmployeeData(dayDiscount: discountCalendarDays, hourlyRate: hourlyRate);

            /// Act
            var result = await _service.ProcessAsync(data);

            /// Assert
            result.Should().NotBeNullOrEmpty();
            result.Single().Employees.Should().NotBeNullOrEmpty();
            result.Single().TotalDiscount.Should().Be(totalValueDiscountInDepartment);
            result.Single().Employees.Single().MissingDays.Should().Be(discountDays);
        }

        [Fact]
        public async Task HoursNotWorkedShouldBeDeducted()
        {
            /// Arrange
            var discountHours = 20;
            var hourlyRate = 110.97;
            var totalValueDiscountInDepartment = hourlyRate * discountHours;
            var data = CreateEmployeeData(outputTime: 16, hourlyRate: hourlyRate);

            /// Act
            var result = await _service.ProcessAsync(data);

            /// Assert
            result.Should().NotBeNullOrEmpty();
            result.Single().Employees.Should().NotBeNullOrEmpty();
            result.Single().TotalDiscount.Should().Be(totalValueDiscountInDepartment);
            result.Single().Employees.Single().DebitHours.Should().Be(discountHours);
        }

        [Fact]
        public async Task HoursExtrasWorkedShouldBePaid()
        {
            /// Arrange
            var extraHours = 20;
            var hourlyRate = 110.97;
            var totalValueExtraInDepartment = hourlyRate * extraHours;
            var data = CreateEmployeeData(outputTime: 18, hourlyRate: hourlyRate, onlyBusinessDays: true);

            /// Act
            var result = await _service.ProcessAsync(data);

            /// Assert
            result.Should().NotBeNullOrEmpty();
            result.Single().Employees.Should().NotBeNullOrEmpty();
            result.Single().TotalExtra.Should().Be(totalValueExtraInDepartment);
            result.Single().Employees.Single().ExtraHours.Should().Be(extraHours);
        }

        private static IDictionary<string, IEnumerable<EmployeeData>> CreateEmployeeData(
            int entryTime = 8, 
            int outputTime = 18, 
            int dayDiscount = 0,
            double hourlyRate = 110.97,
            bool onlyBusinessDays = false)
        {
            var employees = new Dictionary<string, IEnumerable<EmployeeData>>();
            var data = Enumerable.Empty<EmployeeData>();
            for (var i = 1; i <= 30 - dayDiscount; i++)
            {
                var date = new DateOnly(2023, 04, i);
                if (onlyBusinessDays && !date.IsBusinessDay()) continue;

                data = data.Append(new EmployeeData
                {
                    Code = 1,
                    Name = "João da Silva",
                    HourlyRate = hourlyRate,
                    Date = date,
                    EntryTime = new TimeOnly(entryTime, 0),
                    OutputTime = new TimeOnly(outputTime, 0),
                    LunchTime = "12:00 - 13:00"
                });
            }
            employees.Add("test-abril-2023.csv", data);

            return employees;
        }
    }
}
