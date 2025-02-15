using TrainMaster.Application.ExtensionError;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserEntity>> AddUserAsync(UserEntity userEntity);
        Task<Result<UserEntity>> UpdateUserAsync(UserEntity userEntity);
        Task DeleteUserAsync(int userId);
        Task<List<UserEntity>> GetAllUsersAsync();
        Task<UserEntity?> GetUserByIdAsync(int id);
    }
}