using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UserManagement.Enities;

namespace UserManagement.Persistance.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(u => u.PasswordHash)
                   .IsRequired();

            builder.Property(u => u.PasswordSalt)
                   .IsRequired();

            builder.Property(u => u.Role)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            // ---------- Seed Data ----------
            //builder.HasData(
            //    new User
            //    {
            //        Id = Guid.NewGuid(),
            //        Username = "admin",
            //        Email = "admin@sdd.com",
            //        PasswordHash = "AdminHashedPasswordHere",
            //        PasswordSalt = "AdminSaltHere",
            //        Role = "Admin",
            //        CreatedAt = DateTime.UtcNow,
            //        IsDeleted = false
            //    },
            //    new User
            //    {
            //        Id = Guid.NewGuid(),
            //        Username = "user",
            //        Email = "user@sdd.com",
            //        PasswordHash = "UserHashedPasswordHere",
            //        PasswordSalt = "UserSaltHere",
            //        Role = "User",
            //        CreatedAt = DateTime.UtcNow,
            //        IsDeleted = false
            //    },
            //    new User
            //    {
            //        Id = Guid.NewGuid(),
            //        Username = "readonly",
            //        Email = "readonly@sdd.com",
            //        PasswordHash = "ReadOnlyHashedPasswordHere",
            //        PasswordSalt = "ReadOnlySaltHere",
            //        Role = "ReadOnlyUser",
            //        CreatedAt = DateTime.UtcNow,
            //        IsDeleted = false
            //    }
            //);
        }
    }
}
