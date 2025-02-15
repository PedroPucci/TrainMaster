using TrainMaser.Infrastracture.Connections;
using TrainMaser.Infrastracture.Repository.Interfaces;
using TrainMaster.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace TrainMaser.Infrastracture.Repository.Request
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> AddUserAsync(UserEntity userEntity)
        {
            if (userEntity is null)
                throw new ArgumentNullException(nameof(userEntity), "User cannot be null");

            var result = await _context.UserEntity.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public UserEntity DeleteUserAsync(UserEntity userEntity)
        {
            var response = _context.UserEntity.Remove(userEntity);
            return response.Entity;
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
        {
            return await _context.UserEntity
                .OrderBy(accountUser => accountUser.Email)
                .Select(accountUser => new UserEntity
                {
                    Id = accountUser.Id,
                    Email = accountUser.Email
                }).ToListAsync();
        }

        public async Task<UserEntity?> GetUserByIdAsync(int? id)
        {
            return await _context.UserEntity.FirstOrDefaultAsync(userEntity => userEntity.Id == id);
        }

        public UserEntity UpdateUserAsync(UserEntity userEntity)
        {
            var response = _context.UserEntity.Update(userEntity);
            return response.Entity;
        }
    }
}