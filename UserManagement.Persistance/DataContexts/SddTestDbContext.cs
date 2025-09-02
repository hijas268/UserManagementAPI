using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Enities;
using UserManagement.Persistance.Configuration;

namespace UserManagement.Persistance.DataContexts
{
    public class SddTestDbContext : DbContext
    {
        public SddTestDbContext(DbContextOptions<SddTestDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<AuditTrail> AuditTrails => Set<AuditTrail>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
           // modelBuilder.ApplyConfiguration(new Configurations.AuditTrailConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
