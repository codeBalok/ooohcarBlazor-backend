using ooohCar.Application.Features.Products.Commands.AddEdit;
using ooohCar.Application.Features.Products.Queries.GetAllPaged;
using ooohCar.Application.Requests.Catalog;
using ooohCar.Shared.Wrapper;
using System.Threading.Tasks;

namespace ooohCar.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<string> ExportToExcelAsync();
    }
}