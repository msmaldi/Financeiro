using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Msmaldi.AspNetCore.GuIdentity.EntityFrameworkCore;
using Msmaldi.Financeiro.Website.Data.Configurations;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data
{
    public class FinanceiroDbContext : GuIdentityDbContext<User>
    {
        public DbSet<CDBComCDI> CDBsComCDI { get; private set; }
        public DbSet<Feriado> Feriados { get; private set; }
        public DbSet<DIOver> TaxasDIOver { get; private set; }

        public FinanceiroDbContext(DbContextOptions<FinanceiroDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CDBComCDIEntityTypeConfiguration());
            builder.ApplyConfiguration(new ResgateCDBComCDIEntityTypeConfiguration());
            builder.ApplyConfiguration(new DIOverEntityTypeConfiguration());
            builder.ApplyConfiguration(new FeriadoEntityTypeConfiguration());

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
