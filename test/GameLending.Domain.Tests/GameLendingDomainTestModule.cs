using GameLending.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace GameLending
{
    [DependsOn(
        typeof(GameLendingEntityFrameworkCoreTestModule)
        )]
    public class GameLendingDomainTestModule : AbpModule
    {

    }
}