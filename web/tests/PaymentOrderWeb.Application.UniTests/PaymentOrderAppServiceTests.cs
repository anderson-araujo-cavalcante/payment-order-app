using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using PaymentOrderWeb.Application.Concrets;
using PaymentOrderWeb.Application.Interfaces;
using PaymentOrderWeb.Domain.Entities;
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
            var exception = await Assert.ThrowsAsync<AggregateException>(async () => await _appService.ProcessAsync(new List<IFormFile>() { file }));

            /// Assert
            exception.Should().NotBeNull();
            exception.InnerException.Should().NotBeNull();
            exception.InnerException.Message.Should().BeEquivalentTo($"Planila {file.FileName} incosistente.");
        }

        [Theory]
        [InlineData("Nome;Código;Valor hora;Data;Entrada;Saída;Almoço", "João da Silva;1;R$ 110,97;01/04/2022;08:00:00;18:00:00;12:00 - 13:00")]
        [InlineData("Código;Nome;Valor hora;Data;Entrada;Saída", "João da Silva;R$ 110,97;01/04/2022;08:00:00;18:00:00")]
        [InlineData("Código;NomeCompleto;Valor hora;Data;Entrada;Saída;Almoço", "João da Silva;R$ 110,97;01/04/2022;08:00:00;18:00:00;12:00 - 13:00")]
        public async Task Should_ValidateInconsistentColuns(string head, string line)
        {
            /// Arrange
            var content = $"{head}\r\n{line}";
            var file = CreateFormFile(content);

            /// Act
            var exception = await Assert.ThrowsAsync<AggregateException>(async () => await _appService.ProcessAsync(new List<IFormFile>() { file }));

            /// Assert
            exception.Should().NotBeNull();
            exception.InnerException.Should().NotBeNull();
            exception.InnerException.Message.Should().BeEquivalentTo($"Planila {file.FileName} incosistente.");
        }

        [Theory]
        [InlineData("Código;Nome;Valor hora;Data;Entrada;Saída;Almoço", "1;João da Silva;R$ 110,97;01/04/2022;08:00:00;18:00:00;12:00 - 13:00")]
        [InlineData("Codigo;Nome;Valor hora;Data;Entrada;Saída;Almoço", "1;João da Silva;R$ 110,97;01/04/2022;08:00:00;18:00:00;12:00 - 13:00")]
        [InlineData("Código;Nome;Valor hora;Data;Entrada;Saída;Almoco", "1;João da Silva;R$ 110,97;01/04/2022;08:00:00;18:00:00;12:00 - 13:00")]
        [InlineData("Código;Nome;Valor hora;Data;Entrada;Saida;Almoco", "1;João da Silva;R$ 110,97;01/04/2022;08:00:00;18:00:00;12:00 - 13:00")]
        public async Task Should_ReturnSuccess(string head, string line)
        {
            /// Arrange
            var content = $"{head}\r\n{line}";
            var file = CreateFormFile(content);

            /// Act
            await _appService.ProcessAsync(new List<IFormFile>() { file });

            /// Assert
            _service.Verify(x => x.Process1Async(It.IsAny<IDictionary<string, IEnumerable<EmployeeData>>>()), Times.Once);
        }

        [Fact]
        public async Task MultipleFiles_Should_ReturnSuccess()
        {
            /// Arrange
            var files = CreateMultpleFiles();

            /// Act
            await _appService.ProcessAsync(files);

            /// Assert
            _service.Verify(x => x.Process1Async(It.IsAny<IDictionary<string, IEnumerable<EmployeeData>>>()), Times.Exactly(1));
        }

        private static IFormFile CreateFormFile(string content, string departmentName = "test")
        {
            var referenceDate = DateTime.Now;
            var fileName = $"{departmentName}-{referenceDate.Month}-{referenceDate.Year}.csv";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
            return file;
        }

        private static IEnumerable<IFormFile> CreateMultpleFiles(int totalFiles = 10, int totalLines = 30)
        {
            IList<IFormFile> files = new List<IFormFile>();
            var delimiter = ";";

            for (int i = 1; i < totalFiles; i++)
            {
                string content = "Código;Nome;Valor hora;Data;Entrada;Saída;Almoço";
                for (int x = 1; x < totalLines; x++)
                {
                    var line = $"{x}{delimiter}{"tst" + x}{delimiter}{decimal.Zero}{delimiter}{DateTime.Now.Date.ToString("dd/MM/yyyy")}{delimiter}{"8:00"}{delimiter}{"18:00"}{delimiter}{"12:00 - 13:00"}";
                    content += $"\r\n{line}";
                }
                files.Add(CreateFormFile(content, "tst" + i));
            }

            return files;
        }
    }
}