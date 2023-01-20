using TobeDev.FirstProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TobeDev.FirstProject.Web.Models.Clients;
using TobeDev.FirstProject.Web.Data.Entity;

namespace TobeDev.FirstProject.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly DatabaseContext databaseContext;

        public ClientsController(ILogger<ClientsController> logger, DatabaseContext databaseContext)
        {
            _logger = logger;
            this.databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public async Task<IActionResult> Index()
        {
            var clientListViewModel = new ClientListViewModel();

            var clients = await databaseContext.Clients.ToListAsync();
            clientListViewModel.Clients = MapClientsToViewModel(clients);

            return View(clientListViewModel);
        }

        public async Task<IActionResult> Client(int id)
        {
            var clientViewModel = new ClientViewModel();

            var client = await databaseContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if (client != null)
                clientViewModel = MapClientToViewModel(client);

            return View(clientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Client(ClientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Client client = MapViewModelToClient(model);
            if (model.Id > 0)
                databaseContext.Clients.Update(client);
            else
                await databaseContext.Clients.AddAsync(client);

            await databaseContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var clientViewModel = new ClientViewModel();

            var client = await databaseContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if (client != null)
                clientViewModel = MapClientToViewModel(client);


            return View(clientViewModel);
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var clientViewModel = new ClientViewModel();

            var client = await databaseContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if (client != null)
            {
                databaseContext.Clients.Remove(client);
                databaseContext.SaveChanges();
            }


            return RedirectToAction("Index");
        }


        private Client MapViewModelToClient(ClientViewModel clientViewModel)
        {
            return new Client
            {
                BirthDate = clientViewModel.BirthDate,
                FirstName = clientViewModel.FirstName,
                Id = clientViewModel.Id,
                LastName = clientViewModel.LastName
            };
        }

        private IEnumerable<ClientViewModel> MapClientsToViewModel(List<Client> clients)
        {
            return clients?.Select(client => MapClientToViewModel(client));
        }

        private static ClientViewModel MapClientToViewModel(Client client)
        {
            return new ClientViewModel
            {
                BirthDate = client.BirthDate,
                FirstName = client.FirstName,
                Id = client.Id,
                LastName = client.LastName
            };
        }
    }
}