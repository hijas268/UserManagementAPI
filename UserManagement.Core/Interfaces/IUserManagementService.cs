using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UserManagement.Constants.Enums;
using UserManagement.Models;
using UserManagement.Persistance.UnitOfWork.Interfaces;

namespace UserManagement.Core.Interfaces{
 

    public interface IUserManagementService
    {
        Task<OperationResult<ResponseCode, UserDto>> CreateUser(UserDto requestDto);

    }
}
