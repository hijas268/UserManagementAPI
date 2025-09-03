using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using UserManagement.Constants;
using UserManagement.Constants.Enums;
using UserManagement.Core.Interfaces;
using UserManagement.Enities;
using UserManagement.Models;
using UserManagement.Persistance.DataContexts;
using UserManagement.Persistance.UnitOfWork.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace UserManagement.Core.Implementation
{
    public class UserService : IUserService
    {
        private readonly SddTestDbContext _db;
        public UserService(SddTestDbContext db) { _db = db; }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username && !u.IsDeleted);
            if (user == null) return null;

            if (!VerifyPassword(password, user.PasswordHash)) return null;
            return user;
        }

        private bool VerifyPassword(string password, string hashed)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashed);
        }

        public async Task<User> CreateAsync(User user, string currentUser)
        {
            try {
            //user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                //user.CreatedBy = currentUser;
               
                // user.LastModifiedBy = currentUser;
                _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(long id, string currentUser, string ip)
        {
            var user = await GetByIdAsync(id);
            if (user == null) return;
            user.IsDeleted = true;
            //user.LastModifiedBy = currentUser;
            //user.LastModifiedIp = ip;
            user.LastModifiedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _db.Users.Where(u => !u.IsDeleted).ToListAsync();

        public async Task<User?> GetByIdAsync(long id) =>
            await _db.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        public async Task<User> UpdateAsync(User user, string currentUser, string ip)
        {
            //user.LastModifiedBy = currentUser;
            //user.LastModifiedIp = ip;
            user.LastModifiedAt = DateTime.UtcNow;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }



}

