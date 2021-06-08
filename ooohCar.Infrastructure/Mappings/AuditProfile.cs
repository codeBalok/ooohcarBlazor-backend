using AutoMapper;
using ooohCar.Application.Models.Audit;
using ooohCar.Application.Responses.Audit;

namespace ooohCar.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}