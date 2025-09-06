using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; } 
        public string PasswordHash { get; set; } 
        public string PasswordSalt { get; set; } 
        public int RoleId { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public DateTime? LastModifiedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; } 
    }
    // DTO for creating user
    public class CreateUserRequest
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!; // plaintext input
        public int RoleId { get; set; }  // Admin/User/ReadOnlyUser
    }
    public class UpdateProfileDto
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
