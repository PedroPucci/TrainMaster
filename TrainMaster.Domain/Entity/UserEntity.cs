using TrainMaster.Domain.General;

namespace TrainMaster.Domain.Entity
{
    public class UserEntity : BaseEntity
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; } = true;
    }
}