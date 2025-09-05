using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Interfaces;
using UserManagement.Enities;
using UserManagement.Models;
using UserManagement.Persistance.Repositories.Interfaces;
using UserManagement.Persistance.UnitOfWork.Interfaces;

namespace UserManagement.Core.Implementation
{
    public class AuditTrailService : IAuditTrailService
    {

        private readonly IAuditTrailRepository _auditTrailRepository;
        public AuditTrailService(IAuditTrailRepository auditTrailRepository)
        {
            _auditTrailRepository = auditTrailRepository;
        }

            public async Task LogAsync(string action, string performedBy, string entityName, string? entityId, string? ipAddress)
            {
                var log = new AuditTrail
                {
                    Action = action,
                    PerformedBy = performedBy,
                    EntityName = entityName,
                    EntityId = entityId,
                    PerformedAt = DateTime.UtcNow,
                    IpAddress = ipAddress
                };

                await _auditTrailRepository.AddAsync(log);
               
            }

        public async Task<(IEnumerable<AuditTrailDto> Logs, int TotalCount)> GetLogsAsync(string? search, int page, int pageSize)
        {
            var query = _auditTrailRepository.GetAllAsync().Result;

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(l => l.Action.Contains(search));//|| l.PerformedBy.Contains(search));
            }

            var totalCount = query.Count();

            //var logs = query
            //    .OrderByDescending(l => l.PerformedAt)
            //    .Skip((page - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToList();
            var logs =  query
    .OrderByDescending(l => l.PerformedAt)
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .Select(l => new AuditTrailDto
    {
        Id = l.Id,
        Action = l.Action,
        EntityName = l.EntityName,
        EntityId = l.EntityId,
        PerformedBy=l.PerformedBy,
        PerformedAt = l.PerformedAt,
        IpAddress = l.IpAddress,
   
    })
    .ToList();

            return (logs, totalCount);
        }

      
    }

}