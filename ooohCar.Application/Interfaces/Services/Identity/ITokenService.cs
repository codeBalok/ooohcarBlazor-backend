using ooohCar.Application.Interfaces.Common;
using ooohCar.Application.Requests.Identity;
using ooohCar.Application.Responses.Identity;
using ooohCar.Shared.Wrapper;
using System.Threading.Tasks;

namespace ooohCar.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}