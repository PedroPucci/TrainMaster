using TrainMaser.Infrastracture.Repository.RepositoryUoW;
using TrainMaster.Application.Services;

namespace TrainMaster.Application.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IRepositoryUoW _repositoryUoW;
        private UserService userService;
        private ProfilePessoalService profilePessoalService;

        public UnitOfWorkService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public UserService UserService
        {
            get
            {
                if (userService is null)
                    userService = new UserService(_repositoryUoW);
                return userService;
            }
        }

        public ProfilePessoalService ProfilePessoalService
        {
            get
            {
                if (profilePessoalService is null)
                    profilePessoalService = new ProfilePessoalService(_repositoryUoW);
                return profilePessoalService;
            }
        }
    }
}