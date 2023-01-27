using System.ComponentModel.DataAnnotations;

namespace TobeDev.FirstProject.Web.Models.Fornecedores
{
    public class FornecedorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Razão Social é obrigatório")]
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
    }
}
