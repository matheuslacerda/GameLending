using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Volo.Abp.AspNetCore.ExceptionHandling;

namespace GameLending
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "The type is used as a type argument.")]
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<GameLendingHttpApiHostModule>();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

#if DEBUG
            services.Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = true;
            });
#endif
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
