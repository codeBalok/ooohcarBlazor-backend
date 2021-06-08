using ooohCar.Application.Features.Documents.Commands.AddEdit;
using ooohCar.Application.Features.Documents.Queries.GetAll;
using ooohCar.Application.Requests.Documents;
using ooohCar.Shared.Wrapper;
using System.Threading.Tasks;

namespace ooohCar.Client.Infrastructure.Managers.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}