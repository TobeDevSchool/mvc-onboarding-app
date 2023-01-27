using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TobeDev.FirstProject.Web.Data.Entity;
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

        [HttpPost]
        public async Task<IActionResult> CreateFornecedor(FornecedorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Console.WriteLine(model.RazaoSocial);

            Fornecedor fornecedor = MapViewModelToFornecedor(model);

            if (model.Id > 0)
                databaseContext.Fornecedores.Update(fornecedor);
            else
                await databaseContext.Fornecedores.AddAsync(fornecedor);

            await databaseContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var fornecedor = await databaseContext.Fornecedores.FindAsync(id);

                databaseContext.Fornecedores.Remove(fornecedor);
                databaseContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        private Fornecedor MapViewModelToFornecedor(FornecedorViewModel fornecedorViewModel)
        {
            return new Fornecedor
            {
                RazaoSocial = fornecedorViewModel.RazaoSocial,
                CNPJ = fornecedorViewModel.CNPJ,
                Id = fornecedorViewModel.Id
            };
        }

    }
}
