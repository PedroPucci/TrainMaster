using System.Text.Json.Serialization;
using TrainMaster.Domain.Enums;
using TrainMaster.Domain.General;
using TrainMaster.Domain.ValueObject;

namespace TrainMaster.Domain.Entity
{
    public class PessoalProfileEntity : BaseEntity
    {
        public Name Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public EGenderStatus EGenderStatus { get; set; }
        public EMaritalStatus EMaritalStatus { get; set; }

        [JsonIgnore]
        public UserEntity? User { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public AddressEntity? Address { get; set; }
    }
}