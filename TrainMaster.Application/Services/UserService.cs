using BCrypt.Net;
using Serilog;
using System.Text.RegularExpressions;
using TrainMaster.Application.ExtensionError;
using TrainMaster.Application.Services.Interfaces;
using TrainMaster.Domain.Dto;
using TrainMaster.Domain.Entity;
using TrainMaster.Infrastracture.Repository.RepositoryUoW;
using TrainMaster.Infrastracture.Security.Cryptography;
using TrainMaster.Infrastracture.Security.Token.Access;
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
                    return Result<UserEntity>.Error("The provided CPF is already in use.");
                }

                userEntity.ModificationDate = DateTime.UtcNow;
                userEntity.Email = userEntity.Email?.Trim().ToLower();
                userEntity.Password = crypto.HashPassword(userEntity.Password);
                userEntity.Cpf = userEntity.Cpf;
                userEntity.IsActive = true;
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
                throw new InvalidOperationException("Error to add a new User");
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
                throw new InvalidOperationException("Error to delete a User.");
            }
            finally
            {
                Log.Error(LogMessages.DeleteUserSuccess());
                transaction.Dispose();
            }
        }

        public async Task<List<UserDto>> Get()
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                List<UserDto> userEntities = await _repositoryUoW.UserRepository.Get();
                _repositoryUoW.Commit();
                return userEntities;
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.GetAllUserError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Error to loading the list User");
            }
            finally
            {
                Log.Error(LogMessages.GetAllUserSuccess());
                transaction.Dispose();
            }
        }

        public async Task<List<UserDto>> GetPaginated(int pageNumber, int pageSize)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                List<UserDto> userEntities = await _repositoryUoW.UserRepository.GetPaginated(pageNumber, pageSize);
                _repositoryUoW.Commit();
                return userEntities;
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.GetAllUserError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Error to loading the list of users");
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
                throw new InvalidOperationException("Error to loading the list User Actives");
            }
            finally
            {
                Log.Error(LogMessages.GetAllUserSuccess());
                transaction.Dispose();
            }
        }

        public async Task<Result<UserEntity>> Update(UserUpdateDto userCreateUpdateDto)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var userById = await _repositoryUoW.UserRepository.GetById(userCreateUpdateDto.Id);
                if (userById is null)
                    throw new InvalidOperationException("Error updating User");

                //userById.Cpf = userCreateUpdateDto.Cpf;
                userById.Cpf = Regex.Replace(userCreateUpdateDto.Cpf, "[^0-9]", "");
                userById.Email = userCreateUpdateDto.Email;
                userById.ModificationDate = DateTime.UtcNow;

                _repositoryUoW.UserRepository.Update(userById);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                return Result<UserEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.UpdatingErrorUser(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Error updating User", ex);
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public async Task<Result<UserEntity>> ChangePassword(string email, string currentPassword, string newPassword)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var crypto = new BCryptoAlgorithm();
                var normalizedEmail = email?.Trim().ToLower();

                var user = await _repositoryUoW.UserRepository.GetByEmail(normalizedEmail);

                if (user is null)
                {
                    Log.Error(LogMessages.UserNotFound());
                    return Result<UserEntity>.Error("User not found.");
                }

                var isPasswordValid = crypto.VerifyPassword(currentPassword, user.Password);
                if (!isPasswordValid)
                {
                    Log.Error(LogMessages.PasswordInvalid());
                    return Result<UserEntity>.Error("Incorrect current password.");
                }

                user.Password = crypto.HashPassword(newPassword);
                user.ModificationDate = DateTime.UtcNow;

                _repositoryUoW.UserRepository.Update(user);
                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                Log.Information(LogMessages.UpdatingSuccessPassword());
                return Result<UserEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Error updating password: {ex.Message}");
                await transaction.RollbackAsync();
                throw new InvalidOperationException("Error updating the user's password.");
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public async Task<Result<UserEntity>> UpdatePasswordByEmail(string email, string hashedPassword)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var normalizedEmail = email?.Trim().ToLower();

                var user = await _repositoryUoW.UserRepository.GetByEmail(normalizedEmail);

                if (user is null)
                {
                    Log.Error(LogMessages.UserNotFound());
                    return Result<UserEntity>.Error("Usuário não encontrado.");
                }

                user.Password = hashedPassword;
                user.ModificationDate = DateTime.UtcNow;

                _repositoryUoW.UserRepository.Update(user);
                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                Log.Information($"Senha atualizada com sucesso para o e-mail: {email}");
                return Result<UserEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Erro ao atualizar a senha para o e-mail: {email} - {ex.Message}");
                await transaction.RollbackAsync();
                throw new InvalidOperationException("Erro ao atualizar a senha.");
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public async Task<Result<UserEntity>> GetById(int id)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var user = await _repositoryUoW.UserRepository.GetById(id);
                if (user == null)
                {
                    Log.Error($"Usuário com ID {id} não encontrado.");
                    return Result<UserEntity>.Error("Usuário não encontrado.");
                }

                user.Email = user.Email.Trim().ToLower();
                //user.Cpf = user.Cpf.Trim();
                user.Cpf = FormatCpf(user.Cpf);
                _repositoryUoW.Commit();
                return Result<UserEntity>.Okedit(user);
            }
            catch (Exception ex)
            {
                Log.Error($"Erro ao buscar usuário por ID: {ex.Message}");
                transaction.Rollback();
                throw new InvalidOperationException("Erro ao buscar usuário por ID.", ex);
            }
            finally
            {
                transaction.Dispose();
            }
        }

        private string FormatCpf(string cpf)
        {
            cpf = cpf?.Trim();

            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
                return cpf;

            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
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