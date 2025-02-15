using TrainMaser.Infrastracture.Repository.RepositoryUoW;
using TrainMaster.Application.ExtensionError;
using TrainMaster.Application.Services.Interfaces;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryUoW _repositoryUoW;

        public UserService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public Task<Result<UserEntity>> AddUserAsync(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserEntity>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserEntity>> UpdateUserAsync(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }
    }
}