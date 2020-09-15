using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace GameLending.EntityFrameworkCore
{
    public static class GameLendingDbContextModelCreatingExtensions
    {
        public static void ConfigureGameLending(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(GameLendingConsts.DbTablePrefix + "YourEntities", GameLendingConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}