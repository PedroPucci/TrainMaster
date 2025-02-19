using TrainMaster.Application.ExtensionError;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Application.Services.Interfaces
{
    public interface IProfilePessoal
    {
        Task<Result<PessoalProfileEntity>> AddPessoalProfileAsync(PessoalProfileEntity pessoalProfileEntity);
        Task<Result<PessoalProfileEntity>> UpdatePessoalProfileAsync(PessoalProfileEntity pessoalProfileEntity);
        Task DeletePessoalProfileAsync(int pessoalProfileEntity);
        Task<List<PessoalProfileEntity>> GetAllPessoalProfilesAsync();
    }
}