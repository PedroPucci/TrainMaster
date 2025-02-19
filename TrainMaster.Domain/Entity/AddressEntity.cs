using TrainMaster.Domain.General;

namespace TrainMaster.Domain.Entity
{
    public class AddressEntity : BaseEntity
    {
        public int PessoalProfileId { get; set; }
        public string AddressType { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string? Complement { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        // Navigation Property (assuming a relationship with PessoalProfile entity)
        //public PessoalProfileEntity? PessoalProfile { get; set; }
    }
}