using TrainMaster.Domain.Entity;

namespace TrainMaser.Infrastracture.Repository.Interfaces
{
    public interface IPessoalProfileRepository
    {
        Task<PessoalProfileEntity> Add(PessoalProfileEntity pessoalProfileEntity);
        PessoalProfileEntity Update(PessoalProfileEntity pessoalProfileEntity);
        PessoalProfileEntity Delete(PessoalProfileEntity pessoalProfileEntity);
        Task<List<PessoalProfileEntity>> Get();
        Task<PessoalProfileEntity?> GetById(int? id);
    }
}