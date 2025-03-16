using TrainMaster.Application.ExtensionError;
using TrainMaster.Domain.Dto;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserEntity>> Add(UserEntity userEntity);
        Task<Result<UserEntity>> Update(UserCreateUpdateDto userCreateUpdateDto);
        Task Delete(int userId);
        Task<List<UserDto>> Get();
        Task<List<UserDto>> GetPaginated(int pageNumber, int pageSize);
        Task<List<UserEntity>> GetAllActives();
    }
}