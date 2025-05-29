using Microsoft.EntityFrameworkCore.Storage;
using TrainMaster.Infrastracture.Repository.Interfaces;

namespace TrainMaster.Infrastracture.Repository.RepositoryUoW
{
    public interface IRepositoryUoW
    {
        IUserRepository UserRepository { get; }
        IPessoalProfileRepository PessoalProfileRepository { get; }
        IAddressRepository AddressRepository { get; }
        IEducationLevelRepository EducationLevelRepository { get; }
        IProfessionalProfileRepository ProfessionalProfileRepository { get; }
        ICourseRepository CourseRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        ITeamRepository TeamRepository { get; }
        IHistoryPasswordRepository HistoryPasswordRepository { get; }
        ICourseAvaliationRepository CourseAvaliationRepository { get; }

        Task SaveAsync();
        void Commit();
        IDbContextTransaction BeginTransaction();
    }
}