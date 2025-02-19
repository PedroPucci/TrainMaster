using TrainMaster.Domain.Entity;

namespace TrainMaser.Infrastracture.Repository.Interfaces
{
    public interface IPessoalProfileRepository
    {
        Task<PessoalProfileEntity> AddPessoalProfileAsync(PessoalProfileEntity pessoalProfileEntity);
        PessoalProfileEntity UpdatePessoalProfileAsync(PessoalProfileEntity pessoalProfileEntity);
        PessoalProfileEntity DeletePessoalProfileAsync(PessoalProfileEntity pessoalProfileEntity);
        Task<List<PessoalProfileEntity>> GetAllPessoalProfilesAsync();
        Task<PessoalProfileEntity?> GetUPessoalProfileByIdAsync(int? id);
    }
}