using TrainMaster.Application.ExtensionError;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserEntity>> Add(UserEntity userEntity);
        Task<Result<UserEntity>> Update(UserEntity userEntity);
        Task Delete(int userId);
        Task<List<UserEntity>> Get();
        Task<List<UserEntity>> GetAllActives();
    }
}