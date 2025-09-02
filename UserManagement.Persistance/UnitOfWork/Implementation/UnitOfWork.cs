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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public IGenericRepository<User> User
        {
            get
            {
                _userRepository ??= new Lazy<IGenericRepository<User>>(() => new GenericRepository<User>(_context));
                return _userRepository.Value;
            }
        }
    }
}
