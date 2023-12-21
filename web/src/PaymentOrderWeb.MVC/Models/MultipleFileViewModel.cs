using System.ComponentModel.DataAnnotations;

namespace PaymentOrderWeb.MVC.Models
{
    public class MultipleFileViewModel
    {
        [Required(ErrorMessage = "Selecione o(s) arquivo(s).")]
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
