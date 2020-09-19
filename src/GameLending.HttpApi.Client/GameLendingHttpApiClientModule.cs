using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace GameLending
{
    [DependsOn(
        typeof(GameLendingApplicationContractsModule),
        typeof(AbpAccountHttpApiClientModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpPermissionManagementHttpApiClientModule),
        typeof(AbpTenantManagementHttpApiClientModule),
        typeof(AbpFeatureManagementHttpApiClientModule)
    )]
    public class GameLendingHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Check.NotNull")]
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Check.NotNull(context, nameof(context));

            context.Services.AddHttpClientProxies(
                typeof(GameLendingApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
