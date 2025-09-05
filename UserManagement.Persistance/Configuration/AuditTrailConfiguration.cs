using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Enities;

namespace UserManagement.Persistance.Configuration
{
    public class AuditTrailConfiguration : IEntityTypeConfiguration<AuditTrail>
    {
        public void Configure(EntityTypeBuilder<AuditTrail> builder)
        {
 

            // Primary key
            builder.HasKey(a => a.Id);

            // Properties
            builder.Property(a => a.Action)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.EntityName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.EntityId)
                .HasMaxLength(50);

            builder.Property(a => a.PerformedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.IpAddress)
                .HasMaxLength(50);

            //builder.Property(a => a.PerformedAt)
            //    .IsRequired()
            //    .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(a => a.PerformedAt);
            builder.HasIndex(a => a.PerformedBy);
        }
    }
}
