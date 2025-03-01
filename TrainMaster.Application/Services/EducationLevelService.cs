using TrainMaser.Infrastracture.Repository.RepositoryUoW;
using TrainMaster.Application.ExtensionError;
using TrainMaster.Application.Services.Interfaces;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Application.Services
{
    public class EducationLevelService : IEducationLevelService
    {
        private readonly IRepositoryUoW _repositoryUoW;

        public EducationLevelService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public Task<Result<EducationLevelEntity>> Add(EducationLevelEntity educationLevelEntity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int educationLevelId)
        {
            throw new NotImplementedException();
        }

        public Task<List<EducationLevelEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Result<EducationLevelEntity>> Update(EducationLevelEntity educationLevelEntity)
        {
            throw new NotImplementedException();
        }
    }
}