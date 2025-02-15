using TrainMaster.Domain.Entity;

namespace TrainMaser.Infrastracture.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> AddUserAsync(UserEntity userEntity);
        UserEntity UpdateUserAsync(UserEntity userEntity);
        UserEntity DeleteUserAsync(UserEntity userEntity);
        Task<List<UserEntity>> GetAllUsersAsync();
        Task<UserEntity?> GetUserByNameAsync(string? name);
        Task<UserEntity?> GetUserByIdAsync(int? id);
    }
}