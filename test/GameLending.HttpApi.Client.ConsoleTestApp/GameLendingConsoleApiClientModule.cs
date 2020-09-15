using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace GameLending.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(GameLendingHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class GameLendingConsoleApiClientModule : AbpModule
    {
        
    }
}
