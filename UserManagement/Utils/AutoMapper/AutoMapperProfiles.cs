using AutoMapper;
using UserManagement.Enities;
using UserManagement.Models;

namespace UserManagement.Utils.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
          
            MapService();
           

        }

        private void MapService()
        {
            CreateMap<UserDto, User>();
        }
    }
}
