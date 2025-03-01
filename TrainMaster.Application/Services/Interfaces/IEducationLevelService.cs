using TrainMaster.Application.ExtensionError;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Application.Services.Interfaces
{
    public interface IEducationLevelService
    {
        Task<Result<EducationLevelEntity>> Add(EducationLevelEntity educationLevelEntity);
        Task Delete(int educationLevelId);
        Task<List<EducationLevelEntity>> Get();
        Task<Result<EducationLevelEntity>> Update(EducationLevelEntity educationLevelEntity);
    }
}