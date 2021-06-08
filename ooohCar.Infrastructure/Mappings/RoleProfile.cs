using AutoMapper;
using ooohCar.Application.Responses.Identity;
using Microsoft.AspNetCore.Identity;

namespace ooohCar.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, IdentityRole>().ReverseMap();
        }
    }
}