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
using UserManagement.Persistance.UnitOfWork.Implementation;
using Microsoft.AspNetCore.Http;

namespace UserManagement.Core.Implementation
{
    public class UserService : IUserService
    {
        private readonly SddTestDbContext _db;
        private readonly IAuditTrailService _auditTrailService;
        public UserService(SddTestDbContext db, IAuditTrailService auditTrailService) {
            _db = db;
            _auditTrailService = auditTrailService;
        }

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
            await _auditTrailService.LogAsync(
            "CreateUser",
    currentUser,
    "User",
                user.Id.ToString(),""
    
);
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
            await _auditTrailService.LogAsync(
"DeleteUser",
currentUser,
"User",
    user.Id.ToString(), ""

);
        }
        public async Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(
        string? search, int? role, int page, int pageSize)
        {
            var query = _db.Users.Where(x=>x.IsDeleted==false); // returns IQueryable<User>

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u => u.Username.Contains(search) || u.Email.Contains(search));
            }

            if (role!=0 && role!=null)
            {
                query = query.Where(u => u.RoleId == role);
            }

            var totalCount = await query.CountAsync();

            var users = await query
                .OrderBy(u => u.Username)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
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
            await _auditTrailService.LogAsync(
            "UpdateUser",
            currentUser,
            "User",
            user.Id.ToString(), ""
            );
            return user;
        }

        public async Task<User> UpdateProfile(long id, UpdateProfileDto user, string currentuser)
        {
            try
            {
                var userinfo = await _db.Users.FindAsync(id);
                userinfo.Id = id;
                userinfo.Username = user.Username;
                userinfo.Email = user.Email;
                userinfo.LastModifiedAt = DateTime.UtcNow;
                _db.Users.Update(userinfo);
                await _db.SaveChangesAsync();
                await _auditTrailService.LogAsync(
                "UpdateUser",
                currentuser,
                "User",
                userinfo.Id.ToString(), ""
                );
                return userinfo;
            }
            catch(Exception ex) {
                throw ex;
            }
 
        }
    }



}

