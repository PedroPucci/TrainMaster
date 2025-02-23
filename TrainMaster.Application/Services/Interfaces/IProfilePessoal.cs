using TrainMaster.Application.ExtensionError;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Application.Services.Interfaces
{
    public interface IProfilePessoal
    {
        Task<Result<PessoalProfileEntity>> Add(PessoalProfileEntity pessoalProfileEntity);
        Task<Result<PessoalProfileEntity>> Update(PessoalProfileEntity pessoalProfileEntity);
        Task Delete(int pessoalProfileEntity);
        Task<List<PessoalProfileEntity>> Get();
    }
}