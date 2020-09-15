using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace GameLending.Data
{
    /* This is used if database provider does't define
     * IGameLendingDbSchemaMigrator implementation.
     */
    public class NullGameLendingDbSchemaMigrator : IGameLendingDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}