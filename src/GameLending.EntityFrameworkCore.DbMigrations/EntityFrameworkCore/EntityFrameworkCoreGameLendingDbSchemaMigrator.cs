using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GameLending.Data;
using Volo.Abp.DependencyInjection;

namespace GameLending.EntityFrameworkCore
{
    public class EntityFrameworkCoreGameLendingDbSchemaMigrator
        : IGameLendingDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreGameLendingDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the GameLendingMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<GameLendingMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}