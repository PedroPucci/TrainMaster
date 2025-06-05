using Microsoft.EntityFrameworkCore;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Infrastracture.Connections
{
    public static class DataModelConfiguration
    {
        public static void ConfigureModels(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.Cpf).HasMaxLength(14);
                entity.Property(u => u.IsActive).IsRequired();

                entity.HasOne(u => u.PessoalProfile)
                      .WithOne(p => p.User)
                      .HasForeignKey<PessoalProfileEntity>(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.ProfessionalProfile)
                      .WithOne(p => p.User)
                      .HasForeignKey<ProfessionalProfileEntity>(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Courses)
                      .WithOne(c => c.User)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.Department)
                      .WithOne(d => d.User)
                      .HasForeignKey<DepartmentEntity>(d => d.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(u => u.HistoryPasswords)
                     .WithOne(ph => ph.User)
                     .HasForeignKey(ph => ph.UserId)
                     .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PessoalProfileEntity>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FullName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.DateOfBirth).IsRequired();
                entity.Property(p => p.EGenderStatus).IsRequired().HasMaxLength(50);
                entity.Property(p => p.EMaritalStatus).IsRequired().HasMaxLength(50);

                entity.HasOne(p => p.Address)
                      .WithOne(a => a.PessoalProfile)
                      .HasForeignKey<AddressEntity>(a => a.PessoalProfileId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AddressEntity>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.PostalCode).HasMaxLength(50);
                entity.Property(a => a.Street).HasMaxLength(255);
                entity.Property(a => a.Neighborhood).HasMaxLength(255);
                entity.Property(a => a.City).HasMaxLength(100);
                entity.Property(a => a.Uf).HasMaxLength(100);
            });

            modelBuilder.Entity<ProfessionalProfileEntity>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.JobTitle).HasMaxLength(255);
                entity.Property(p => p.YearsOfExperience);
                entity.Property(p => p.Skills);
                entity.Property(p => p.Certifications);

                entity.HasOne(p => p.User)
                      .WithOne(u => u.ProfessionalProfile)
                      .HasForeignKey<ProfessionalProfileEntity>(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.EducationLevel)
                      .WithOne(e => e.ProfessionalProfile)
                      .HasForeignKey<EducationLevelEntity>(e => e.ProfessionalProfileId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EducationLevelEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(255);
                entity.Property(e => e.Institution).HasMaxLength(255);
                entity.Property(e => e.StartedAt);
                entity.Property(e => e.EndeedAt);

                entity.HasOne(e => e.ProfessionalProfile)
                      .WithOne(p => p.EducationLevel)
                      .HasForeignKey<EducationLevelEntity>(e => e.ProfessionalProfileId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CourseEntity>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired();
                entity.Property(c => c.Description);
                entity.OwnsOne(c => c.Period, period =>
                {
                    period.Property(p => p.StartDate)
                          .HasColumnName("StartDate")
                          .IsRequired();

                    period.Property(p => p.EndDate)
                          .HasColumnName("EndDate")
                          .IsRequired();
                });

                entity.HasOne(c => c.User)
                      .WithMany(u => u.Courses)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DepartmentEntity>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired().HasMaxLength(255);
                entity.Property(d => d.Description).HasMaxLength(500);

                entity.HasOne(d => d.User)
                      .WithOne(u => u.Department)
                      .HasForeignKey<DepartmentEntity>(d => d.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<HistoryPasswordEntity>(entity =>
            {
                entity.HasKey(ph => ph.Id);
                entity.Property(ph => ph.OldPassword).IsRequired().HasMaxLength(255);

                entity.HasOne(ph => ph.User)
                      .WithMany(u => u.HistoryPasswords)
                      .HasForeignKey(ph => ph.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TeamEntity>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired().HasMaxLength(255);
                entity.Property(t => t.Description).HasMaxLength(500);

                entity.HasOne(t => t.Department)
                      .WithMany(d => d.Teams)
                      .HasForeignKey(t => t.DepartmentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CourseAvaliationEntity>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Rating).IsRequired();
                entity.Property(a => a.Comment).HasColumnType("text");
                entity.Property(a => a.ReviewDate).IsRequired();

                entity.HasOne(a => a.Course)
                      .WithMany(c => c.Avaliations)
                      .HasForeignKey(a => a.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CourseActivitieEntity>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Title).IsRequired().HasMaxLength(255);
                entity.Property(a => a.Description).HasColumnType("text");
                //entity.Property(a => a.StartDate).IsRequired();
                //entity.Property(a => a.DueDate).IsRequired();
                entity.OwnsOne(a => a.Period, period =>
                {
                    period.Property(p => p.StartDate)
                          .HasColumnName("StartDate")
                          .IsRequired();

                    period.Property(p => p.EndDate)
                          .HasColumnName("DueDate")
                          .IsRequired();
                });
                entity.Property(a => a.MaxScore).IsRequired();

                entity.HasOne(a => a.Course)
                      .WithMany(c => c.Activities)
                      .HasForeignKey(a => a.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}