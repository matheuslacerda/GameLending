using GameLending.EntityFrameworkCore;
using GameLending.MultiTenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace GameLending
{
    [DependsOn(
        typeof(GameLendingHttpApiModule),
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(GameLendingApplicationModule),
        typeof(GameLendingEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAspNetCoreSerilogModule)
        )]
    public class GameLendingHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Check.NotNull")]
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Check.NotNull(context, nameof(context));

            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            ConfigureConventionalControllers();
            ConfigureAuthentication(context, configuration);
            ConfigureLocalization();
            ConfigureCache();
            ConfigureVirtualFileSystem(context);
            ConfigureRedis(context, configuration, hostingEnvironment);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context);
        }

        private void ConfigureCache()
        {
            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "GameLending:";
            });
        }

        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            /*if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<GameLendingDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}GameLending.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<GameLendingDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}GameLending.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<GameLendingApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}GameLending.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<GameLendingApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}GameLending.Application"));
                });
            }*/
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(GameLendingApplicationModule).Assembly);
            });
        }

        private static void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = true;
                    options.ApiName = "GameLending";
                });
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "GameLending API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                });
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }

        private static void ConfigureRedis(
            ServiceConfigurationContext context,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "GameLending-Protection-Keys");
            }
        }

        private static void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "GameLending API");
            });

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
