using TrainMaster.Application.ExtensionError;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Application.Services.Interfaces
{
    public interface IAddressService
    {
        Task<Result<AddressEntity>> FindAddressByZipCode(string postalCode);
    }
}
