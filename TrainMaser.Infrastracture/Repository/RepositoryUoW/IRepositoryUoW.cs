using Microsoft.EntityFrameworkCore.Storage;
using TrainMaser.Infrastracture.Repository.Interfaces;

namespace TrainMaser.Infrastracture.Repository.RepositoryUoW
{
    public interface IRepositoryUoW
    {
        IUserRepository UserRepository { get; }

        Task SaveAsync();
        void Commit();
        IDbContextTransaction BeginTransaction();
    }
}