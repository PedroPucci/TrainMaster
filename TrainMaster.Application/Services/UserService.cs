using Microsoft.EntityFrameworkCore;
using Serilog;
using TrainMaser.Infrastracture.Repository.RepositoryUoW;
using TrainMaser.Infrastracture.Repository.Security.Cryptography;
using TrainMaser.Infrastracture.Security.Token.Access;
using TrainMaster.Application.ExtensionError;
using TrainMaster.Application.Services.Interfaces;
using TrainMaster.Domain.Entity;
using TrainMaster.Shared.Logging;
using TrainMaster.Shared.Validator;

namespace TrainMaster.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryUoW _repositoryUoW;

        public UserService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public async Task<Result<UserEntity>> Add(UserEntity userEntity)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var crypto = new BCryptoAlgorithm();
                var isValidUser = await IsValidUserRequest(userEntity);

                if (!isValidUser.Success)
                {
                    Log.Error(LogMessages.InvalidUserInputs());
                    return Result<UserEntity>.Error(isValidUser.Message);
                }

                if (await UniqueCpf(userEntity.Cpf))
                {
                    Log.Error("CPF already exists in the system.");
                    return Result<UserEntity>.Error("Message: The provided CPF is already in use.");
                }

                userEntity.ModificationDate = DateTime.UtcNow;
                userEntity.Email = userEntity.Email?.Trim().ToLower();
                userEntity.Password = crypto.HashPassword(userEntity.Password);
                userEntity.Cpf = userEntity.Cpf;
                var result = await _repositoryUoW.UserRepository.Add(userEntity);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                var tokenGenerator = new TokenService();
                var token = tokenGenerator.GenerateToken(userEntity.Id.ToString(), userEntity.Email);

                return Result<UserEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.AddingUserError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to add a new User");
            }
            finally
            {
                Log.Error(LogMessages.AddingUserSuccess());
                transaction.Dispose();
            }
        }

        public async Task Delete(int userId)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var userToDelete = await _repositoryUoW.UserRepository.GetById(userId);                
                if (userToDelete is not null)
                    _repositoryUoW.UserRepository.UpdateByActive(userToDelete.Id, userToDelete.IsActive);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.DeleteUserError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to delete a User.");
            }
            finally
            {
                Log.Error(LogMessages.DeleteUserSuccess());
                transaction.Dispose();
            }
        }

        public async Task<List<UserEntity>> Get()
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                List<UserEntity> userEntities = await _repositoryUoW.UserRepository.Get();
                _repositoryUoW.Commit();
                return userEntities;
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.GetAllUserError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to loading the list User");
            }
            finally
            {
                Log.Error(LogMessages.GetAllUserSuccess());
                transaction.Dispose();
            }
        }

        public async Task<List<UserEntity>> GetAllActives()
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                List<UserEntity> userEntities = await _repositoryUoW.UserRepository.GetAllActives();
                _repositoryUoW.Commit();
                return userEntities;
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.GetAllUserError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to loading the list User Actives");
            }
            finally
            {
                Log.Error(LogMessages.GetAllUserSuccess());
                transaction.Dispose();
            }
        }

        public async Task<Result<UserEntity>> Update(UserEntity userEntity)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var userById = await _repositoryUoW.UserRepository.GetById(userEntity.Id);
                if (userById is null)
                    throw new InvalidOperationException("Message: Error updating User");
                
                userById.Email = userEntity.Email;
                userById.ModificationDate = DateTime.UtcNow;
                userById.IsActive = userEntity.IsActive;

                _repositoryUoW.UserRepository.Update(userById);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                return Result<UserEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.UpdatingErrorUser(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error updating User", ex);
            }
            finally
            {
                transaction.Dispose();
            }
        }

        private async Task<Result<UserEntity>> IsValidUserRequest(UserEntity userEntity)
        {
            var requestValidator = await new UserRequestValidator().ValidateAsync(userEntity);
            if (!requestValidator.IsValid)
            {
                string errorMessage = string.Join(" ", requestValidator.Errors.Select(e => e.ErrorMessage));
                errorMessage = errorMessage.Replace(Environment.NewLine, "");
                return Result<UserEntity>.Error(errorMessage);
            }

            return Result<UserEntity>.Ok();
        }

        private async Task<bool> UniqueCpf(string cpf)
        {
            return await _repositoryUoW.UserRepository.GetByCpf(cpf) is not null;
        }

    }
}