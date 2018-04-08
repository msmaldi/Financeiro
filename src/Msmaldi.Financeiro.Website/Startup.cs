﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Msmaldi.Financeiro.Website.Data;
using Msmaldi.Financeiro.Website.Entities;
using Msmaldi.Financeiro.Website.Services;
using Msmaldi.AspNetCore.GuIdentity;
using Msmaldi.Financeiro.Website.Identity;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Msmaldi.Financeiro.Website.Data.Seeders;
using Msmaldi.Financeiro.Website.HostedServices;
using Microsoft.Extensions.Hosting;
using Msmaldi.Financeiro.Data.Seeder;

namespace Msmaldi.Financeiro.Website
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            Environment = env;
        }

        public Startup(IHostingEnvironment environment, IConfiguration configuration) 
        {
            this.Environment = environment;
                this.Configuration = configuration;
               
        }
                public IHostingEnvironment Environment { get; } 
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FinanceiroDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, GuIdentityRole>()
                .AddEntityFrameworkStores<FinanceiroDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PortugueseIdentityErrorDescriber>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSingleton<FeriadoSeeder>((service) =>
            {
                var scope = service.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<FinanceiroDbContext>();
                return new FeriadoSeeder(context);
            });
            
            services.AddSingleton<TaxasDIOverSeeder>((service) =>
            {
                var scope = service.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<FinanceiroDbContext>();
                return new TaxasDIOverSeeder(context);
            });

            services.AddSingleton<IHostedService, FeriadosUpdaterService>();
            services.AddSingleton<IHostedService, DIOverUpdaterService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            var ptBRCulture = new CultureInfo("pt-BR");
            var supportedCultures = new[]
            {
                ptBRCulture
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(ptBRCulture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            
        }
    }
}
