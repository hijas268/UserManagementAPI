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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SddTestDbContext context) :base(context)
        {
                
        }
    }
}
