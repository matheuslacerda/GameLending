using GameLending.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace GameLending.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(GameLendingEntityFrameworkCoreDbMigrationsModule),
        typeof(GameLendingApplicationContractsModule)
        )]
    public class GameLendingDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
