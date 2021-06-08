using ooohCar.Application.Interfaces.Common;

namespace ooohCar.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}