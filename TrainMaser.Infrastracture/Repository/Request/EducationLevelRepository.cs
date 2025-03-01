using TrainMaser.Infrastracture.Connections;
using TrainMaser.Infrastracture.Repository.Interfaces;
using TrainMaster.Domain.Entity;

namespace TrainMaser.Infrastracture.Repository.Request
{
    public class EducationLevelRepository : IEducationLevelRepository
    {
        private readonly DataContext _context;

        public EducationLevelRepository(DataContext context)
        {
            _context = context;
        }

        public Task<EducationLevelEntity> Add(EducationLevelEntity educationLevelEntity)
        {
            throw new NotImplementedException();
        }

        public EducationLevelEntity Delete(EducationLevelEntity educationLevelEntity)
        {
            throw new NotImplementedException();
        }

        public Task<List<EducationLevelEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<EducationLevelEntity?> GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public EducationLevelEntity Update(EducationLevelEntity educationLevelEntity)
        {
            throw new NotImplementedException();
        }
    }
}