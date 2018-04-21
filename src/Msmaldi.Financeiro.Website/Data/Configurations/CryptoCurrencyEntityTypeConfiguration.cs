using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data.Configurations
{
    public class CryptoCurrencyEntityTypeConfiguration : IEntityTypeConfiguration<CryptoCurrency>
    {
        public void Configure(EntityTypeBuilder<CryptoCurrency> builder)
        {
            builder.Property(d => d.Id).HasColumnType("varchar(8)");
            builder.Property(d => d.Name).HasColumnType("varchar(32)").IsRequired();
        }
    }
}