using DeleteMe.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DeleteMe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();

            // Skapar en DbContextOptionsBuilder som hjälper till att konfigurera
            // alternativ för ApplicationDbContext.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Hämtar anslutningssträngen "DefaultConnection" från inställningarna i
            // config-objektet.
            var connectionString = config.GetConnectionString("DefaultConnection");

            // Använder anslutningssträngen för att konfigurera SQL Server som
            // databas för ApplicationDbContext.
            options.UseSqlServer(connectionString);

            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                dbContext.Database.Migrate();
            }

            Console.WriteLine("Database has been created!");

        }
    }
}
