using AutoMapper;
using ooohCar.Application.Features.Brands.Commands.AddEdit;
using ooohCar.Application.Features.Brands.Queries.GetAll;
using ooohCar.Application.Features.Brands.Queries.GetById;
using ooohCar.Domain.Entities.Catalog;

namespace ooohCar.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<AddEditBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsResponse, Brand>().ReverseMap();
        }
    }
}