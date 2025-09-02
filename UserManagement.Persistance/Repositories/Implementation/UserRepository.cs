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
    public class UserRepository : IUserRepository
    {
      
        private readonly SddTestDbContext _db;
        public UserRepository(SddTestDbContext db) { _db = db; }

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _db.Users.Where(u => !u.IsDeleted).ToListAsync();

        public async Task<User?> GetByIdAsync(long id) =>
            await _db.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        public async Task AddAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(User user)
        {
            user.IsDeleted = true;
            await UpdateAsync(user);
        }
    }
}
