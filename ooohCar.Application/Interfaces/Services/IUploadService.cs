using ooohCar.Application.Requests;

namespace ooohCar.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}