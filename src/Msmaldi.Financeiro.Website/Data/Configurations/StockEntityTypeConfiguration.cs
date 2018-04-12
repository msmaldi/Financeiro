using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data.Configurations
{
    public class StockEntityTypeConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.Property(d => d.Symbol).HasColumnType("varchar(16)");
            builder.HasKey(d => d.Symbol);

            builder.Property(d => d.Currency).HasColumnType("varchar(16)");

            builder.HasMany(d => d.StockQuotesDaily);
        }
    }
}