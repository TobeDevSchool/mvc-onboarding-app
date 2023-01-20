using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TobeDev.FirstProject.Web.Models.Fornecedores;

namespace TobeDev.FirstProject.Web.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public FornecedoresController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<IActionResult> Index()
        {
            var listaDeFornecedoresViewModel = new ListaFornecedoresViewModel();
            listaDeFornecedoresViewModel.Fornecedores = new List<FornecedorViewModel>();

            var fornecedores = await databaseContext.Fornecedores.ToListAsync();

            listaDeFornecedoresViewModel.Fornecedores = fornecedores?.Select(fornecedor => new FornecedorViewModel()
            {
                CNPJ = fornecedor.CNPJ,
                Id = fornecedor.Id,
                RazaoSocial = fornecedor.RazaoSocial
            });


            return View(listaDeFornecedoresViewModel);
        }
    }
}
