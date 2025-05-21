using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TrainMaster.Domain.General;

namespace TrainMaster.Domain.Entity
{
    public class CourseEntity : BaseEntity
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public UserEntity? User { get; set; }
        public int UserId { get; set; }
    }
}