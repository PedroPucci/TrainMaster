using TrainMaster.Domain.Entity;

namespace TrainMaser.Infrastracture.Repository.Interfaces
{
    public interface IAddressRepository
    {
        Task<AddressEntity> AddAddressAsync(AddressEntity addressEntity);
        AddressEntity UpdateAddressAsync(AddressEntity addressEntity);
        AddressEntity DeleteAddressAsync(AddressEntity addressEntity);
        Task<List<AddressEntity>> GetAllAddressesAsync();
        Task<AddressEntity?> GetAddressByIdAsync(int? id);
    }
}