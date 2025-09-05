using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Interfaces;
using UserManagement.Enities;
using UserManagement.Persistance.UnitOfWork.Interfaces;

namespace UserManagement.Core.Implementation
{
    public class AuditTrailService : IAuditTrailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditTrailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.AuditTrails.AddAsync(log);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IEnumerable<AuditTrail> Logs, int TotalCount)> GetLogsAsync(string? search, int page, int pageSize)
        {
            var query = _unitOfWork.AuditTrails.GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(l => l.Action.Contains(search) || l.PerformedBy.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var logs = await query
                .OrderByDescending(l => l.PerformedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (logs, totalCount);
        }
    }