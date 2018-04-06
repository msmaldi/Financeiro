using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data.Configurations
{
    public class DIOverEntityTypeConfiguration : IEntityTypeConfiguration<DIOver>
    {
        public void Configure(EntityTypeBuilder<DIOver> builder)
        {
            builder.Property(d => d.Data).HasColumnType("date");
            builder.HasKey(d => d.Data);
        }
    }
}