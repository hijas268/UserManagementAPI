using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Enities;
using UserManagement.Persistance.DataContexts;
using UserManagement.Persistance.Repositories.Implementation;
using UserManagement.Persistance.Repositories.Interfaces;
using UserManagement.Persistance.UnitOfWork.Interfaces;

namespace UserManagement.Persistance.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SddTestDbContext _context;
        private Lazy<IGenericRepository<User>> _userRepository;
        public IUserRepository _Users { get; private set; }
        private Lazy<IGenericRepository<AuditTrail>>? _auditTrails;



        //public UnitOfWork(SddTestDbContext context,IUserRepository userRepository, IAuditTrailRepository auditTrailRepository)
        //{
        //    _context = context;
        //    _Users = userRepository;
          
        //}
        //public void Dispose()
        //{
        //    _context.Dispose();
        //}

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
        public IGenericRepository<AuditTrail> AuditTrails
        {
            get
            {
                _auditTrails ??= new Lazy<IGenericRepository<AuditTrail>>(
                    () => new GenericRepository<AuditTrail>(_context)
                );
                return _auditTrails.Value;
            }
        }
        public IGenericRepository<User> Users
        {
            get
            {
                _userRepository ??= new Lazy<IGenericRepository<User>>(() => new GenericRepository<User>(_context));
                return _userRepository.Value;
            }
        }

 
    }
}
