using EazyCoreObjs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.Data
{
    public class ImageContext : DbContext
    {
        private readonly string connectionString;
        //private readonly IConfiguration configuration;
        public ImageContext()
        {
        }

        public ImageContext(DbContextOptions<ImageContext> options) : base(options)
        {
        }

        public ImageContext(DbContextOptions<ImageContext> options, IConfiguration configuration) : base(options)
        {
            //_connectionString = ConfigurationExtensions.GetConnectionString(configuration, "EazyPushCon").ToString();
            connectionString = configuration.GetConnectionString("EazyImageCon").ToString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var _connectionString = "Server=DLINKS;Database=SampleDBAX;Trusted_Connection=false;MultipleActiveResultSets=true;User ID=ApiLogin;Password=abc@123;";
                optionsBuilder.UseSqlServer(connectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    });
                base.OnConfiguring(optionsBuilder);
            }
        }

        public DbSet<AcctMandates> AcctMandates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcctMandates>().HasKey(k => k.Sequence);

        }
    }
}
