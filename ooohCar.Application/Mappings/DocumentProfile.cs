using AutoMapper;
using ooohCar.Application.Features.Documents.Commands.AddEdit;
using ooohCar.Domain.Entities;

namespace ooohCar.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
        }
    }
}