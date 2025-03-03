using TrainMaster.Domain.Entity;

namespace TrainMaster.Infrastracture.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> Add(UserEntity userEntity);
        UserEntity Update(UserEntity userEntity);
        UserEntity Delete(UserEntity userEntity);
        //Task<List<UserEntity>> Get();
        Task<List<UserEntity>> GetPaginated(int pageNumber, int pageSize);
        Task<List<UserEntity>> GetAllActives();
        Task<UserEntity?> GetById(int? id);
        Task<UserEntity?> GetByCpf(string? cpf);
        UserEntity UpdateByActive(int userId, bool isActive);
    }
}