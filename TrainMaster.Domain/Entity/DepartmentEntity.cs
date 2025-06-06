using System.Text.Json.Serialization;
using TrainMaster.Domain.General;
using TrainMaster.Domain.ValueObject;

namespace TrainMaster.Domain.Entity
{
    public class DepartmentEntity : BaseEntity
    {
        public Name Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public UserEntity? User { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public List<TeamEntity> Teams { get; set; } = [];
    }
}