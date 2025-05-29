using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Infrastracture.Connections
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<UserEntity> UserEntity { get; set; }
        public DbSet<PessoalProfileEntity> PessoalProfileEntity { get; set; }
        public DbSet<ProfessionalProfileEntity> ProfessionalProfileEntity { get; set; }
        public DbSet<AddressEntity> AddressEntity { get; set; }
        public DbSet<EducationLevelEntity> EducationLevelEntity { get; set; }
        public DbSet<CourseEntity> CourseEntity { get; set; }
        public DbSet<DepartmentEntity> DepartmentEntity { get; set; }
        public DbSet<TeamEntity> TeamEntity { get; set; }
        public DbSet<HistoryPasswordEntity> HistoryPasswordEntity { get; set; }
        public DbSet<CourseAvaliationEntity> CourseAvaliationEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DataModelConfiguration.ConfigureModels(modelBuilder);
        }
    }
}