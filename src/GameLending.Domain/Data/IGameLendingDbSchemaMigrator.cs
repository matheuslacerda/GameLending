using System.Threading.Tasks;

namespace GameLending.Data
{
    public interface IGameLendingDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
