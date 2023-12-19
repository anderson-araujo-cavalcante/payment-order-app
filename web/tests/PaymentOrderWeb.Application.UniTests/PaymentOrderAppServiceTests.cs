using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using PaymentOrderWeb.Application.Concrets;
using PaymentOrderWeb.Application.Interfaces;
using PaymentOrderWeb.Domain.Interfaces.Services;

namespace PaymentOrderWeb.Application.UnitTests
{
    public class PaymentOrderAppServiceTests
    {
        private readonly IPaymentOrderAppService _appService;
        private readonly Mock<IPaymentOrderService> _service;

        public PaymentOrderAppServiceTests()
        {
            _service = new Mock<IPaymentOrderService>();
            _appService = new PaymentOrderAppService(paymentOrderService: _service.Object);
        }

        [Theory]
        [InlineData("2023/04/01")]
        [InlineData("32/04/2023")]
        [InlineData("01/13/2023")]
        public async Task Should_ValidateInconsistentDate(string dateInconsistent)
        {
            /// Arrange
            var content = $"Código;Nome;Valor hora;Data;Entrada;Saída;Almoço\r\n1;João da Silva;R$ 110,97;{dateInconsistent};08:00:00;18:00:00;12:00 - 13:00\r\n1;João da Silva;R$ 110,97;02/04/2022;08:00:00;18:00:00;12:00 - 13:00";
            var file = CreateFormFile(content);

            /// Act
            var exception = await Assert.ThrowsAsync<AggregateException>(async () => await _appService.Process(new List<IFormFile>() { file }));

            /// Assert
            exception.Should().NotBeNull();
            exception.InnerException.Should().NotBeNull();
            exception.InnerException.Message.Should().BeEquivalentTo($"Planila {file.FileName} incosistente.");
        }

        [Fact]
        public async Task Should_ValidateInconsistentColuns()
        {
            /// Arrange
            var content = $"NomeCódigo;Valor hora;Data;Entrada;Saída;Almoço\r\nJoão da Silva;1;R$ 110,97;01/04/2022;08:00:00;18:00:00;12:00 - 13:00";
            var file = CreateFormFile(content);

            /// Act
            var exception = await Assert.ThrowsAsync<AggregateException>(async () => await _appService.Process(new List<IFormFile>() { file }));

            /// Assert
            exception.Should().NotBeNull();
            exception.InnerException.Should().NotBeNull();
            exception.InnerException.Message.Should().BeEquivalentTo($"Planila {file.FileName} incosistente.");
        }

        private static IFormFile CreateFormFile(string content)
        {
            var fileName = "test.csv";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
            return file;
        }
    }
}