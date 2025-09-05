using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Enities
{
    [Table("Users", Schema = "UserManagement")]
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; } 
        public string PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public int RoleId { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public DateTime? LastModifiedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
 
    }
}
