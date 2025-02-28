using BCrypt.Net;
using TrainMaser.Infrastracture.Repository.RepositoryUoW;
using TrainMaser.Infrastracture.Repository.Security.Cryptography;
using TrainMaser.Infrastracture.Security.Token.Access;
using TrainMaster.Application.Services;

namespace TrainMaster.Application.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IRepositoryUoW _repositoryUoW;
        private readonly TokenService _tokenService;
        private readonly BCryptoAlgorithm _crypto;

        private UserService userService;
        private ProfilePessoalService profilePessoalService;
        private AuthService authService;
        private AddressService addressService;

        public UnitOfWorkService(IRepositoryUoW repositoryUoW, TokenService tokenService, BCryptoAlgorithm crypto)
        {
            _repositoryUoW = repositoryUoW;
            _tokenService = tokenService;
            _crypto = crypto;
        }

        public AddressService AddressService
        {
            get
            {
                if (addressService is null)
                    addressService = new AddressService(_repositoryUoW);
                return addressService;
            }
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

        public AuthService AuthService
        {
            get
            {
                if (authService is null)
                    authService = new AuthService(_repositoryUoW.UserRepository, _tokenService, _crypto);
                return authService;
            }
        }
    }
}