using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace NittietFirstTest.Models
{
    public class MainContextFactory : IDbContextFactory<MainContext>
    {
        public MainContext Create(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainContext>();
            // TODO Replace with string from config somehow
            optionsBuilder.UseSqlServer("Server=tcp:nittietdbserver.database.windows.net,1433;Initial Catalog=NittietTestDB;Persist Security Info=False;User ID=nittietadmin;Password=NittieT91#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return new MainContext(optionsBuilder.Options);
        }
    }
}