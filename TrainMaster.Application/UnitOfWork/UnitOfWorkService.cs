using TrainMaster.Application.Services;
using TrainMaster.Infrastracture.Repository.RepositoryUoW;
using TrainMaster.Infrastracture.Security.Cryptography;
using TrainMaster.Infrastracture.Security.Token.Access;

namespace TrainMaster.Application.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IRepositoryUoW _repositoryUoW;
        private readonly TokenService _tokenService;
        private readonly BCryptoAlgorithm _crypto;

        private UserService userService;
        private ProfilePessoalService profilePessoalService;
        private ProfileProfessionalService profileProfessionalService;
        private AuthService authService;
        private AddressService addressService;
        private EducationLevelService educationLevelService;
        private CourseService courseService;
        private DepartmentService departmentService;
        private TeamService teamService;
        private HistoryPasswordService historyPasswordService;

        public UnitOfWorkService(IRepositoryUoW repositoryUoW, TokenService tokenService, BCryptoAlgorithm crypto)
        {
            _repositoryUoW = repositoryUoW;
            _tokenService = tokenService;
            _crypto = crypto;
        }

        public TeamService TeamService
        {
            get
            {
                if (teamService is null)
                    teamService = new TeamService(_repositoryUoW);
                return teamService;
            }
        }

        public HistoryPasswordService HistoryPasswordService
        {
            get
            {
                if (historyPasswordService is null)
                    historyPasswordService = new HistoryPasswordService(_repositoryUoW);
                return historyPasswordService;
            }
        }

        public DepartmentService DepartmentService
        {
            get
            {
                if (departmentService is null)
                    departmentService = new DepartmentService(_repositoryUoW);
                return departmentService;
            }
        }

        public CourseService CourseService
        {
            get
            {
                if (courseService is null)
                    courseService = new CourseService(_repositoryUoW);
                return courseService;
            }
        }

        public EducationLevelService EducationLevelService
        {
            get
            {
                if (educationLevelService is null)
                    educationLevelService = new EducationLevelService(_repositoryUoW);
                return educationLevelService;
            }
        }

        public ProfileProfessionalService ProfileProfessionalService
        {
            get
            {
                if (profileProfessionalService is null)
                    profileProfessionalService = new ProfileProfessionalService(_repositoryUoW);
                return profileProfessionalService;
            }
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
                    authService = new AuthService(UserService, _repositoryUoW.UserRepository, _tokenService, _crypto);
                return authService;
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
    }
}