using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Candidate.Data
{
    public class PeopleContextFactory : IDesignTimeDbContextFactory<CandidateContext>
    {
        public CandidateContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}Candidate.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new CandidateContext(config.GetConnectionString("ConStr"));
        }
    }


    public class CandidateContext : DbContext
    {
        private string _connectionString;
        public CandidateContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbSet<Candidates> Candidates {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidates>().Property(c => c.Status).HasConversion<int>();
        }
    }
}
