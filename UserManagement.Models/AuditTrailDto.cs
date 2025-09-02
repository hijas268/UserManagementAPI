using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class AuditTrailDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Action { get; set; }
        public string TableName { get; set; } 
        public string? RecordId { get; set; }
        public DateTime ActionDate { get; set; } 
        public string? IpAddress { get; set; }
    }
}
