using System.Text.Json.Serialization;
using TrainMaster.Domain.General;
using TrainMaster.Domain.ValueObject;

namespace TrainMaster.Domain.Entity
{
    public class TeamEntity : BaseEntity
    {
        public Name Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public DepartmentEntity? Department { get; set; }
        public int DepartmentId { get; set; }
    }
}