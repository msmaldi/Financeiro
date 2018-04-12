using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data.Configurations
{
    public class StockQuoteDailyEntityTypeConfiguration : IEntityTypeConfiguration<StockQuoteDaily>
    {
        public void Configure(EntityTypeBuilder<StockQuoteDaily> builder)
        {
            builder.Property(d => d.Symbol).HasColumnType("varchar(16)");
            builder.Property(d => d.Date).HasColumnType("date");
            builder.HasKey(d => new { d.Symbol, d.Date } );
        }
    }
}