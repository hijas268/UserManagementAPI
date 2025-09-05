using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Enities
{
    [Table("AuditTrails", Schema = "UserManagement")]
    public class AuditTrail
    {
        public long Id { get; set; }
        public string Action { get; set; }
        public string PerformedBy { get; set; }
        public string EntityName { get; set; } 
        public string? EntityId { get; set; } 
        public DateTime PerformedAt { get; set; }
        public string? IpAddress { get; set; }
       

    }
}
