using AutoMapper;
using ooohCar.Application.Features.Products.Commands.AddEdit;
using ooohCar.Domain.Entities.Catalog;

namespace ooohCar.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
        }
    }
}