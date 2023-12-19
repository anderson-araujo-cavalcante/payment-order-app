namespace PaymentOrderWeb.MVC.Models
{
    public class MultipleFileViewModel
    {
        public IEnumerable<IFormFile> Files { get; set; }
    }

    //public class SiteDto
    //{
    //   // [Index(0)]
    //    //[Name("SiteId")]
    //    public string SiteId { get; set; }//

    //    //[Index(1)]
    //   //[Name("Name")]
    //    public string Name { get; set; }

    //    //[Index(3)]
    //    //public string Code { get; set; }

    //    //[Index(4)]
    //    //public string CompanyName { get; set; }

    //    //[Index(5)]
    //    //public string Location { get; set; }
    //}

    //public sealed class SiteDtoMap : ClassMap<SiteDto>
    //{
    //    public SiteDtoMap()
    //    {
    //        Map(m => m.SiteId).Index(0);
    //        Map(m => m.Name).Index(1);
    //        //Map(m => m.Documento).Name("Documento", "Doc", "CPF");
    //        //Map(m => m.Email1).Name("Email", "Email1", "E-mail", "E-mail1");
    //        //Map(m => m.PessoaId).Ignore();
    //    }
    //}

    //public class SiteDto : Profile
    //{
    //    public string SiteId { get; set; }//

    //    public string Name { get; set; }
    //}
}
