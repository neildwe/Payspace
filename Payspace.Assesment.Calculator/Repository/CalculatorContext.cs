using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payspace.Assesment.Repository
{
    public class CalculatorContext : DbContext
    {
        public CalculatorContext(string ConnectionString) : base(GetOptions(ConnectionString))
        {

        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        public CalculatorContext(DbContextOptions<CalculatorContext> options) : base(options)
        {
        }
        public DbSet<Payspace.Assesment.Entities.PostalCode> PostalCode { get; set; }
        public DbSet<Payspace.Assesment.Entities.Calculations> Calculations { get; set; }
        public DbSet<Payspace.Assesment.Entities.AuditResults> AuditResults { get; set; }
    }
}
