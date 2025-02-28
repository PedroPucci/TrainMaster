using Microsoft.EntityFrameworkCore;
using TrainMaster.Domain.Entity;

namespace TrainMaser.Infrastracture.Connections
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
                entity.HasOne(u => u.PessoalProfile)
                      .WithOne(p => p.User)
                      .HasForeignKey<PessoalProfileEntity>(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PessoalProfileEntity>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FullName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.DateOfBirth).IsRequired();
                entity.Property(p => p.Gender).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Marital).IsRequired().HasMaxLength(50);
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
        }
    }
}