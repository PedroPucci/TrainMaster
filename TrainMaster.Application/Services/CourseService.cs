using TrainMaster.Application.ExtensionError;
using TrainMaster.Application.Services.Interfaces;
using TrainMaster.Domain.Entity;
using TrainMaster.Infrastracture.Repository.RepositoryUoW;

namespace TrainMaster.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepositoryUoW _repositoryUoW;

        public CourseService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public Task<Result<CourseEntity>> Add(CourseEntity courseEntity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Result<CourseEntity>> Update(CourseEntity courseEntity)
        {
            throw new NotImplementedException();
        }
    }
}