using TrainMaster.Application.Services;

namespace TrainMaster.Application.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        UserService UserService { get; }
        AuthService AuthService { get; }
        ProfilePessoalService ProfilePessoalService { get; }
        ProfileProfessionalService ProfileProfessionalService { get; }
        AddressService AddressService { get; }
        EducationLevelService EducationLevelService { get; }
        CourseService CourseService { get; }
    }
}