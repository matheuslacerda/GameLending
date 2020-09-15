using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GameLending.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class GameLendingMigrationsDbContextFactory : IDesignTimeDbContextFactory<GameLendingMigrationsDbContext>
    {
        public GameLendingMigrationsDbContext CreateDbContext(string[] args)
        {
            GameLendingEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<GameLendingMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new GameLendingMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../GameLending.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
