using ooohCar.Application.Responses.Audit;
using ooohCar.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ooohCar.Client.Infrastructure.Managers.Audit
{
    public interface IAuditManager : IManager
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync();

        Task<string> DownloadFileAsync();
    }
}