using Crud.Core.Entities;
using Crud.API.Dtos;
using AutoMapper;
namespace API.Configuration
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();  
        }
    }
}
