using System.Text.Json.Serialization;
using TrainMaster.Domain.General;
using TrainMaster.Domain.ValueObject;

namespace TrainMaster.Domain.Entity
{
    public class EducationLevelEntity : BaseEntity
    {
        public string? Title { get; set; }
        public string? Institution { get; set; }
        public Period Period { get; set; }

        [JsonIgnore]
        public ProfessionalProfileEntity? ProfessionalProfile { get; set; }
        public int ProfessionalProfileId { get; set; }
    }
}