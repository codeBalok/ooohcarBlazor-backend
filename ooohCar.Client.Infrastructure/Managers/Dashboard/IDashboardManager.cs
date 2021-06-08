using ooohCar.Shared.Wrapper;
using System.Threading.Tasks;
using ooohCar.Application.Features.Dashboards.Queries.GetData;

namespace ooohCar.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}