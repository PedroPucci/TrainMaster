using TrainMaster.Domain.Entity;

namespace TrainMaser.Infrastracture.Repository.Interfaces
{
    public interface IEducationLevelRepository
    {
        Task<EducationLevelEntity> Add(EducationLevelEntity educationLevelEntity);
        EducationLevelEntity Update(EducationLevelEntity educationLevelEntity);
        EducationLevelEntity Delete(EducationLevelEntity educationLevelEntity);
        Task<List<EducationLevelEntity>> Get();
        Task<EducationLevelEntity?> GetById(int? id);
    }
}