using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Enities;
using UserManagement.Persistance.DataContexts;
using UserManagement.Persistance.Repositories.Interfaces;

namespace UserManagement.Persistance.Repositories.Implementation
{
    public class AuditTrailRepository :  IAuditTrailRepository
    {

        private readonly SddTestDbContext _db;
        public AuditTrailRepository(SddTestDbContext db) {
            _db = db;
        }

        public async Task<IEnumerable<AuditTrail>> GetAllAsync()
        {
           return await _db.AuditTrails.ToListAsync();
        }
    }
}
