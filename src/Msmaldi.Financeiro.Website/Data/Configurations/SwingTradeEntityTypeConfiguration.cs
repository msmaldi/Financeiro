using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data.Configurations
{
    public class SwingTradeEntityTypeConfiguration : IEntityTypeConfiguration<SwingTrade>
    {
        public void Configure(EntityTypeBuilder<SwingTrade> builder)
        {
            builder.Property(d => d.Symbol).HasColumnType("varchar(16)");
            builder.HasOne(s => s.Stock).WithMany().HasForeignKey(c => c.Symbol);
        }
    }
}