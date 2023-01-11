using Microsoft.EntityFrameworkCore;
using TobeDev.FirstProject.Web.Data.Entity;

namespace TobeDev.FirstProject.Web.Data.Seed
{
    public static class DatabaseSeed
    {
        public static void ExecuteDatabaseSeed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                context.Database.Migrate();
                ClientSeed(context);
                context.SaveChanges();
            }
        }

        private static void ClientSeed(DatabaseContext context)
        {
            var existingClients = context.Clients.Any();
            if (!existingClients)
            {
                var newClients = GetClientList();
                context.Clients.AddRange(newClients);
            }
        }

        private static IEnumerable<Client> GetClientList()
        {
            return Enumerable.Range(1, 3).Select(c => new Client() { 
                FirstName = "Client",
                LastName = c.ToString(),
                BirthDate =  DateTime.Now.AddDays(c).AddMonths(c).AddYears(-c)
            });
        }
    }
}
