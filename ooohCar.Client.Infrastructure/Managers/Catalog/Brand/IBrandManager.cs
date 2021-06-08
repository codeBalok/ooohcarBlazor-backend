using ooohCar.Application.Features.Brands.Queries.GetAll;
using ooohCar.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ooohCar.Application.Features.Brands.Commands.AddEdit;

namespace ooohCar.Client.Infrastructure.Managers.Catalog.Brand
{
    public interface IBrandManager : IManager
    {
        Task<IResult<List<GetAllBrandsResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditBrandCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}