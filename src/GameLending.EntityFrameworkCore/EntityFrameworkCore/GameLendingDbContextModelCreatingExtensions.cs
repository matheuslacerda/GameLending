using GameLending.Amigos;
using GameLending.Jogos;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace GameLending.EntityFrameworkCore
{
    public static class GameLendingDbContextModelCreatingExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Check.NotNull")]
        public static void ConfigureGameLending(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Amigo>(b =>
            {
                b.ToTable(GameLendingConsts.DbTablePrefix + "Amigo", GameLendingConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(p => p.Nome).IsRequired().HasMaxLength(200);
            });

            builder.Entity<Jogo>(b =>
            {
                b.ToTable(GameLendingConsts.DbTablePrefix + "Jogo", GameLendingConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(p => p.Nome).IsRequired().HasMaxLength(200);
                b.HasOne<Amigo>().WithMany().HasForeignKey(p => p.AmigoId);
            });
        }
    }
}