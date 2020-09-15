using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace GameLending.EntityFrameworkCore
{
    [DependsOn(
        typeof(GameLendingEntityFrameworkCoreModule)
        )]
    public class GameLendingEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<GameLendingMigrationsDbContext>();
        }
    }
}
