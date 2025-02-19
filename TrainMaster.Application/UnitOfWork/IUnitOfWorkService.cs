using TrainMaster.Application.Services;

namespace TrainMaster.Application.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        UserService UserService { get; }
        ProfilePessoalService ProfilePessoalService { get; }
    }
}