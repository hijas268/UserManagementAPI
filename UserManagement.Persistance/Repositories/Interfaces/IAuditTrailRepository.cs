using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Enities;

namespace UserManagement.Persistance.Repositories.Interfaces
{
    public interface IAuditTrailRepository 
    {
        Task<IEnumerable<AuditTrail>> GetAllAsync(); 
        Task AddAsync(AuditTrail auditTrail);
 
    }
}
