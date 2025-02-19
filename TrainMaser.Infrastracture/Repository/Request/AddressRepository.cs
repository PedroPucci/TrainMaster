using Microsoft.EntityFrameworkCore;
using TrainMaser.Infrastracture.Connections;
using TrainMaser.Infrastracture.Repository.Interfaces;
using TrainMaster.Domain.Entity;

namespace TrainMaser.Infrastracture.Repository.Request
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DataContext _context;

        public AddressRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<AddressEntity> AddAddressAsync(AddressEntity addressEntity)
        {
            var result = await _context.AddressEntity.AddAsync(addressEntity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public AddressEntity DeleteAddressAsync(AddressEntity addressEntity)
        {
            var response = _context.AddressEntity.Remove(addressEntity);
            return response.Entity;
        }

        public async Task<AddressEntity?> GetAddressByIdAsync(int? id)
        {
            return await _context.AddressEntity.FirstOrDefaultAsync(addressEntity => addressEntity.Id == id);
        }

        public async Task<List<AddressEntity>> GetAllAddressesAsync()
        {
            return await _context.AddressEntity
             .OrderBy(address => address.Id)
             .Select(address => new AddressEntity
             {
                 Id = address.Id,
                 State = address.State,
                 City = address.City,
                 AddressType = address.AddressType,
                 PostalCode = address.PostalCode,
                 Street = address.Street,
                 Complement = address.City
             }).ToListAsync();
        }

        public AddressEntity UpdateAddressAsync(AddressEntity addressEntity)
        {
            var response = _context.AddressEntity.Update(addressEntity);
            return response.Entity;
        }
    }
}