using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace GameLending.EntityFrameworkCore
{
    [DependsOn(
        typeof(GameLendingEntityFrameworkCoreModule)
        )]
    public class GameLendingEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Check.NotNull")]
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Check.NotNull(context, nameof(context));

            context.Services.AddAbpDbContext<GameLendingMigrationsDbContext>();
        }
    }
}
