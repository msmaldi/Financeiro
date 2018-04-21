using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data.Configurations
{
    public class CryptoWalletEntityTypeConfiguration : IEntityTypeConfiguration<CryptoWallet>
    {
        public void Configure(EntityTypeBuilder<CryptoWallet> builder)
        {
            builder.Property(d => d.Label).HasColumnType("varchar(60)").IsRequired();
        }
    }
}