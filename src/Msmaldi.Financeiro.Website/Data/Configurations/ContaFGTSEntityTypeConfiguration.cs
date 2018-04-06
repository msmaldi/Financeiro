using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data.Configurations
{
    public class CDBComCDIEntityTypeConfiguration : IEntityTypeConfiguration<CDBComCDI>
    {
        public void Configure(EntityTypeBuilder<CDBComCDI> builder)
        {
            builder.Property(c => c.DataDaAplicacao).HasColumnType("date");
            builder.Property(c => c.DataDoVencimento).HasColumnType("date");

            var navigation = builder.Metadata.FindNavigation(nameof(CDBComCDI.Resgates));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}