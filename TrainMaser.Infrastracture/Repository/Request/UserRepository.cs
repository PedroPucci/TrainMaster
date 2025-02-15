using TrainMaser.Infrastracture.Connections;
using TrainMaser.Infrastracture.Repository.Interfaces;
using TrainMaster.Domain.Entity;

namespace TrainMaser.Infrastracture.Repository.Request
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public Task<UserEntity> AddUserAsync(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public UserEntity DeleteUserAsync(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserEntity>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity?> GetUserByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity?> GetUserByNameAsync(string? name)
        {
            throw new NotImplementedException();
        }

        public UserEntity UpdateUserAsync(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }
    }
}