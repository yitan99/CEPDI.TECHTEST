using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using CEPDI.TECHTEST.MODELS;
using System.Net;
using System;

namespace CEPDI.TECHTEST.Api.DATA
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// OnConfiguring: Configuración de conexión a base de datos usando appsettings
        /// </summary>
        /// <param name="OptionsBuilder"></param>
        protected override void OnConfiguring
                 (DbContextOptionsBuilder OptionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            OptionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            base.OnConfiguring(OptionsBuilder);
        }
        public DbSet<usuarios> usuarios { get; set; }
        public DbSet<medicamentos> medicamentos { get; set; }
        public DbSet<formasfarmaceuticas> formasfarmaceuticas { get; set; }

    }
}
