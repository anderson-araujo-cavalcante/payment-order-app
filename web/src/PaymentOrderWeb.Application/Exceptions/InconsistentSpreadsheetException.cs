namespace PaymentOrderWeb.Application.Exceptions
{
    public class InconsistentSpreadsheetException : Exception
    {
        private string FileName { get; set; }

        public InconsistentSpreadsheetException() : base() { }

        public InconsistentSpreadsheetException(string fileName) : base()
        {
            FileName = fileName;
        }

        public InconsistentSpreadsheetException(string message, Exception innerException) : base(message, innerException) { }

        public override string Message => $"Planila {FileName} incosistente.";
    }
}
