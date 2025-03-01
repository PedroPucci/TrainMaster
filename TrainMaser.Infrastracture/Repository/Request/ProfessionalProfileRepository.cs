using TrainMaser.Infrastracture.Connections;
using TrainMaser.Infrastracture.Repository.Interfaces;
using TrainMaster.Domain.Entity;

namespace TrainMaser.Infrastracture.Repository.Request
{
    public class ProfessionalProfileRepository : IProfessionalProfileRepository
    {
        private readonly DataContext _context;

        public ProfessionalProfileRepository(DataContext context)
        {
            _context = context;
        }

        public Task<ProfessionalProfileEntity> Add(ProfessionalProfileEntity professionalProfileEntity)
        {
            throw new NotImplementedException();
        }

        public ProfessionalProfileEntity Delete(ProfessionalProfileEntity professionalProfileEntity)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProfessionalProfileEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<ProfessionalProfileEntity?> GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public ProfessionalProfileEntity Update(ProfessionalProfileEntity professionalProfileEntity)
        {
            throw new NotImplementedException();
        }
    }
}