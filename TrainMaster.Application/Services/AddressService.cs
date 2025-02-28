using Newtonsoft.Json;
using RestSharp;
using Serilog;
using TrainMaser.Infrastracture.Repository.RepositoryUoW;
using TrainMaster.Application.ExtensionError;
using TrainMaster.Application.Services.Interfaces;
using TrainMaster.Domain.Entity;

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

        private bool IsValidCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return false;
            cep = cep.Trim();
            return cep.Length == 8 && cep.All(char.IsDigit);
        }
    }
}
