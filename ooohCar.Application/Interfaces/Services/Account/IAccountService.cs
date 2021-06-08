using ooohCar.Application.Interfaces.Common;
using ooohCar.Application.Requests.Identity;
using ooohCar.Shared.Wrapper;
using System.Threading.Tasks;

namespace ooohCar.Application.Interfaces.Services.Account
{
    public interface IAccountService : IService
    {
        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model, string userId);

        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId);

        Task<IResult<string>> GetProfilePictureAsync(string userId);

        Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
    }
}