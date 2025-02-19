using System.Text.Json.Serialization;
using TrainMaster.Domain.General;

namespace TrainMaster.Domain.Entity
{
    public class AddressEntity : BaseEntity
    {
        public string? AddressType { get; set; }
        public string? Street { get; set; }
        public string? Complement { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        [JsonIgnore]
        public PessoalProfileEntity? PessoalProfile { get; set; }
        public int PessoalProfileId { get; set; }
    }
}