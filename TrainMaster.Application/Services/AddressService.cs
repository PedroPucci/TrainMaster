using Newtonsoft.Json;
using RestSharp;
using Serilog;
using TrainMaser.Infrastracture.Repository.RepositoryUoW;
using TrainMaser.Infrastracture.Repository.Security.Cryptography;
using TrainMaster.Application.ExtensionError;
using TrainMaster.Application.Services.Interfaces;
using TrainMaster.Domain.Entity;
using TrainMaster.Shared.Logging;
using TrainMaster.Shared.Validator;

namespace TrainMaster.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepositoryUoW _repositoryUoW;

        public AddressService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public async Task<Result<AddressEntity>> FindAddressByZipCode(string postalCode)
        {
            try
            {
                if (!IsValidCep(postalCode))
                {
                    Log.Error("Postal code is invalid.");
                    return Result<AddressEntity>.Error("Message: Postal Code invalid.");
                }

                string url = $"https://viacep.com.br/ws/{postalCode}/json/";

                var client = new RestClient();
                var request = new RestRequest(url, RestSharp.Method.Get);

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
                {
                    var endereco = JsonConvert.DeserializeObject<AddressEntity>(response.Content);

                    if (endereco == null || string.IsNullOrWhiteSpace(endereco.PostalCode))
                        return Result<AddressEntity>.Error("Message: Postal code not found.");

                    return Result<AddressEntity>.Ok("Postal code found.", endereco);
                }

                return Result<AddressEntity>.Error("Message: Error fetching postal code.");
            }
            catch (Exception ex)
            {
                Log.Error($"Error fetching postal code: {ex.Message}");
                return Result<AddressEntity>.Error("Message: Error occurred while fetching postal code.");
            }
        }

        public async Task<Result<AddressEntity>> Add(AddressEntity addressEntity)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var crypto = new BCryptoAlgorithm();
                var isValidAddress = await IsValidAddressRequest(addressEntity);

                if (!isValidAddress.Success)
                {
                    Log.Error(LogMessages.InvalidAddressInputs());
                    return Result<AddressEntity>.Error(isValidAddress.Message);
                }

                if (!IsValidCep(addressEntity.PostalCode))
                {
                    Log.Error("Postal code is invalid.");
                    return Result<AddressEntity>.Error("Message: Postal Code invalid.");
                }

                addressEntity.ModificationDate = DateTime.UtcNow;
                var result = await _repositoryUoW.AddressRepository.Add(addressEntity);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();               

                return Result<AddressEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.AddingAddressError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to add a new address");
            }
            finally
            {
                Log.Error(LogMessages.AddingAddressSuccess());
                transaction.Dispose();
            }
        }

        public async Task Delete(int userId)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var addressEntity = await _repositoryUoW.AddressRepository.GetById(userId);
                if (addressEntity is not null)
                    _repositoryUoW.AddressRepository.Update(addressEntity);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.DeleteAddressError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to delete a Address.");
            }
            finally
            {
                Log.Error(LogMessages.DeleteAddressSuccess());
                transaction.Dispose();
            }
        }

        public async Task<List<AddressEntity>> Get()
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                List<AddressEntity> addressEntities = await _repositoryUoW.AddressRepository.Get();
                _repositoryUoW.Commit();
                return addressEntities;
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.GetAllAddressError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to loading the list Address");
            }
            finally
            {
                Log.Error(LogMessages.GetAllAddressSuccess());
                transaction.Dispose();
            }
        }

        public async Task<Result<AddressEntity>> Update(AddressEntity addressEntity)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var addressById = await _repositoryUoW.AddressRepository.GetById(addressEntity.Id);
                if (addressById is null)
                    throw new InvalidOperationException("Message: Error updating Address");

                addressById.Street = addressEntity.Street;
                addressById.City = addressEntity.City;
                addressById.Uf = addressEntity.Uf;
                addressById.ModificationDate = DateTime.UtcNow;

                _repositoryUoW.AddressRepository.Update(addressById);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                return Result<AddressEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.UpdatingErrorAddress(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error updating Address", ex);
            }
            finally
            {
                transaction.Dispose();
            }
        }

        private async Task<Result<AddressEntity>> IsValidAddressRequest(AddressEntity addressEntity)
        {
            var requestValidator = await new AddressRequestValidator().ValidateAsync(addressEntity);
            if (!requestValidator.IsValid)
            {
                string errorMessage = string.Join(" ", requestValidator.Errors.Select(e => e.ErrorMessage));
                errorMessage = errorMessage.Replace(Environment.NewLine, "");
                return Result<AddressEntity>.Error(errorMessage);
            }

            return Result<AddressEntity>.Ok();
        }

        private bool IsValidCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return false;
            cep = cep.Trim();
            return cep.Length == 8 && cep.All(char.IsDigit);
        }
    }
}
