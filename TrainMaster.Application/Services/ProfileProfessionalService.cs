using TrainMaster.Application.ExtensionError;
using TrainMaster.Application.Services.Interfaces;
using TrainMaster.Domain.Entity;
using TrainMaster.Infrastracture.Repository.RepositoryUoW;

namespace TrainMaster.Application.Services
{
    public class ProfileProfessionalService : IProfileProfessionalService
    {
        private readonly IRepositoryUoW _repositoryUoW;

        public ProfileProfessionalService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public Task<Result<ProfessionalProfileEntity>> Add(ProfessionalProfileEntity professionalProfileEntity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int professionalProfileEntity)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProfessionalProfileEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Result<ProfessionalProfileEntity>> Update(ProfessionalProfileEntity professionalProfileEntity)
        {
            throw new NotImplementedException();
        }
    }
}