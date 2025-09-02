using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UserManagement.Constants;
using UserManagement.Constants.Enums;
using UserManagement.Core.Interfaces;
using UserManagement.Enities;
using UserManagement.Models;
using UserManagement.Persistance.UnitOfWork.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace UserManagement.Core.Implementation
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UserManagementService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
                
        }
        public async Task<OperationResult<ResponseCode, UserDto>> CreateUser(UserDto requestDto)
        {
            var user = _mapper.Map<User>(requestDto);
            await _unitOfWork.User.AddAsync(user);
            return new OperationResult<ResponseCode, UserDto>(requestDto, ResponseCode.Success,
          new ResponseMessageDto { Message = CustomResponseMessage.Success });
        }
    }
}
