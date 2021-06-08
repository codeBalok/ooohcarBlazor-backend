using AutoMapper;
using ooohCar.Application.Requests.Identity;
using ooohCar.Application.Responses.Identity;

namespace ooohCar.Client.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimsResponse, RoleClaimsRequest>().ReverseMap();
        }
    }
}