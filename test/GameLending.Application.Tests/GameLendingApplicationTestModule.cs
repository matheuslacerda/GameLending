using Volo.Abp.Modularity;

namespace GameLending
{
    [DependsOn(
        typeof(GameLendingApplicationModule),
        typeof(GameLendingDomainTestModule)
        )]
    public class GameLendingApplicationTestModule : AbpModule
    {

    }
}