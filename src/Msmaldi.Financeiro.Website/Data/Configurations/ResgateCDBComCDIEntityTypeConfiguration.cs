using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data.Configurations
{
    public class ResgateCDBComCDIEntityTypeConfiguration : IEntityTypeConfiguration<ResgateCDBComCDI>
    {
        public void Configure(EntityTypeBuilder<ResgateCDBComCDI> builder)
        {
            builder.Property(c => c.Data).HasColumnType("date");
            builder.HasKey(c => new { c.CDBComCDIId, c.Data });
        }
    }
}