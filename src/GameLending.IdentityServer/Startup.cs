using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GameLending
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "The type is used as a type argument")]
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<GameLendingIdentityServerModule>();
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
