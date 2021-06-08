using ooohCar.Domain.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ooohCar.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryAsync<T> Repository<T>() where T : AuditableEntity;

        Task<int> Commit(CancellationToken cancellationToken);

        Task<int> ComitAndRemoveCache(CancellationToken cancellationToken, string cacheKey);

        Task Rollback();
    }
}