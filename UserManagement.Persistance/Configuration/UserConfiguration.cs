using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UserManagement.Enities;
using BCrypt.Net;
using System.Diagnostics;

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

         

            //builder.Property(u => u.CreatedAt)
            //       .HasDefaultValueSql("GETUTCDATE()");

            //builder.Property(u => u.IsDeleted)
            //       .HasDefaultValue(false);

            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            // ---------- Seed Data ----------
            
            //builder.HasData(
            //    new User
            //    {
            //        Id = Guid.NewGuid(),
            //        Username = "admin",
            //        Email = "admin@sdd.com",
            //        PasswordSalt = "AdminSaltHere",
            //        Role = "Admin",
            //        CreatedAt = DateTime.UtcNow,
            //        IsDeleted = false,
            //        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"), // plaintext password hashed
                    
            //    },
            //    new User
            //    {
            //        Id = Guid.NewGuid(),
            //        Username = "user",
            //        Email = "user@sdd.com",
            //        PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
            //        Role = "User",
            //        CreatedAt = DateTime.UtcNow,
            //        IsDeleted = false
            //    },
            //    new User
            //    {
            //        Id = Guid.NewGuid(),
            //        Username = "readonly",
            //        Email = "readonly@sdd.com",
            //        PasswordHash = BCrypt.Net.BCrypt.HashPassword("ReadOnly123!"),
            //        Role = "ReadOnlyUser",
            //        CreatedAt = DateTime.UtcNow,
            //        IsDeleted = false
            //    }
            //);
        }
    }
}
