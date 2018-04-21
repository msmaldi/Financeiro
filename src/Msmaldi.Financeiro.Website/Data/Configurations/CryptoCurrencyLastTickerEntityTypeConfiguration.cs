using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data.Configurations
{
    public class CryptoCurrencyLastTickerEntityTypeConfiguration : IEntityTypeConfiguration<CryptoCurrencyLastTicker>
    {
        public void Configure(EntityTypeBuilder<CryptoCurrencyLastTicker> builder)
        {
            builder.HasKey(d => d.CriptoCurrencyId);
            builder.HasOne(d => d.CriptoCurrency).WithOne(c => c.LastTicker);
        }
    }
}