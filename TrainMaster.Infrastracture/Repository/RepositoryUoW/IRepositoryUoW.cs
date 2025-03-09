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

        Task SaveAsync();
        void Commit();
        IDbContextTransaction BeginTransaction();
    }
}