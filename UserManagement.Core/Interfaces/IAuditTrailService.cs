using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Enities;

namespace UserManagement.Core.Interfaces
{
    public interface IAuditTrailService
    {
        Task LogAsync(string action, string performedBy, string entityName, string? entityId, string? ipAddress);
        Task<(IEnumerable<AuditTrail> Logs, int TotalCount)> GetLogsAsync(string? search, int page, int pageSize);
    }
}
