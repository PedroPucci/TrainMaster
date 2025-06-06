using System.Text.Json.Serialization;
using TrainMaster.Domain.General;
using TrainMaster.Domain.ValueObject;

namespace TrainMaster.Domain.Entity
{
    public class ProfessionalProfileEntity : BaseEntity
    {
        public Name Name { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? Skills { get; set; }
        public string? Certifications { get; set; }

        [JsonIgnore]
        public UserEntity? User { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public EducationLevelEntity? EducationLevel { get; set; }
    }
}