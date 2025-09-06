using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UserManagement.Constants.Enums;
using UserManagement.Enities;
using UserManagement.Models;
using UserManagement.Persistance.UnitOfWork.Interfaces;

namespace UserManagement.Core.Interfaces{
 

    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(long   id);
        Task<User> CreateAsync(User user, string currentUser);
        Task<User> UpdateAsync(User user, string currentUser, string ip);
        Task<User> UpdateProfile(long id,UpdateProfileDto user,string currentuser);
        Task DeleteAsync(long id, string currentUser, string ip);
        Task<User?> AuthenticateAsync(string username, string password);
        Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(
      string? search, int? role, int page, int pageSize);
    }
}
