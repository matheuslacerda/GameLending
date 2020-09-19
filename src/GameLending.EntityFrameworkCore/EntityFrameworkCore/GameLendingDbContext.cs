using GameLending.Amigos;
using GameLending.Jogos;
using GameLending.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace GameLending.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See GameLendingMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class GameLendingDbContext : AbpDbContext<GameLendingDbContext>
    {
        public DbSet<AppUser> Users { get; set; }

        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Amigo> Amigos { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside GameLendingDbContextModelCreatingExtensions.ConfigureGameLending
         */

        public GameLendingDbContext(DbContextOptions<GameLendingDbContext> options)
            : base(options)
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Check.NotNull")]
        protected override void OnModelCreating(ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser

                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the GameLendingEfCoreEntityExtensionMappings class
                 */
            });

            /* Configure your own tables/entities inside the ConfigureGameLending method */

            builder.ConfigureGameLending();
        }
    }
}
