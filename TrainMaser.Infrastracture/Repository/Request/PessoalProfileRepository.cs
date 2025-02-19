using Microsoft.EntityFrameworkCore;
using TrainMaser.Infrastracture.Connections;
using TrainMaser.Infrastracture.Repository.Interfaces;
using TrainMaster.Domain.Entity;

namespace TrainMaser.Infrastracture.Repository.Request
{
    public class PessoalProfileRepository : IPessoalProfileRepository
    {
        private readonly DataContext _context;

        public PessoalProfileRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<PessoalProfileEntity> AddPessoalProfileAsync(PessoalProfileEntity pessoalProfileEntity)
        {
            var result = await _context.PessoalProfileEntity.AddAsync(pessoalProfileEntity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public PessoalProfileEntity DeletePessoalProfileAsync(PessoalProfileEntity pessoalProfileEntity)
        {
            var response = _context.PessoalProfileEntity.Remove(pessoalProfileEntity);
            return response.Entity;
        }

        public async Task<List<PessoalProfileEntity>> GetAllPessoalProfilesAsync()
        {
            return await _context.PessoalProfileEntity
                .OrderBy(pessoalProfile => pessoalProfile.Id)
                .Select(pessoalProfile => new PessoalProfileEntity
                {
                    Id = pessoalProfile.Id,
                    DateOfBirth = pessoalProfile.DateOfBirth,
                    FullName = pessoalProfile.FullName,
                    Gender = pessoalProfile.Gender,
                    Marital = pessoalProfile.Marital
                }).ToListAsync();
        }

        public async Task<PessoalProfileEntity?> GetUPessoalProfileByIdAsync(int? id)
        {
            return await _context.PessoalProfileEntity.FirstOrDefaultAsync(pessoalProfileEntity => pessoalProfileEntity.Id == id);
        }

        public PessoalProfileEntity UpdatePessoalProfileAsync(PessoalProfileEntity pessoalProfileEntity)
        {
            var response = _context.PessoalProfileEntity.Update(pessoalProfileEntity);
            return response.Entity;
        }
    }
}