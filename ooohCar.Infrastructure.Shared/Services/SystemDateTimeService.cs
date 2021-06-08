using ooohCar.Application.Interfaces.Services;
using System;

namespace ooohCar.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}