using ooohCar.Application.Responses.Audit;
using ooohCar.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ooohCar.Application.Interfaces.Services
{
    public interface IAuditService
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId);

        Task<string> ExportToExcelAsync(string userId);
    }
}