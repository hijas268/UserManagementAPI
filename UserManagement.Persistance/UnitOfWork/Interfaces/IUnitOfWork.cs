using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Enities;
using UserManagement.Persistance.Repositories.Interfaces;

namespace UserManagement.Persistance.UnitOfWork.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<User> User { get; }
    }
}
