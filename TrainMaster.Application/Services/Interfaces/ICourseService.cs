using TrainMaster.Application.ExtensionError;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Application.Services.Interfaces
{
    public interface ICourseService
    {
        Task<Result<CourseEntity>> Add(CourseEntity courseEntity);
        Task Delete(int userId);
        Task<List<CourseEntity>> Get();
        //Task<Result<List<CourseEntity>>> Get();
        Task<Result<CourseEntity>> Update(CourseEntity courseEntity);
    }
}