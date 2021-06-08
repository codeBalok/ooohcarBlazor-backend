using AutoMapper;
using ooohCar.Application.Models.Identity;
using ooohCar.Application.Responses.Identity;

namespace ooohCar.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, CarUser>().ReverseMap();
            CreateMap<ChatUserResponse, CarUser>().ReverseMap()
                .ForMember(dest => dest.EmailAddress, source => source.MapFrom(source => source.Email)); //Specific Mapping
        }
    }
}