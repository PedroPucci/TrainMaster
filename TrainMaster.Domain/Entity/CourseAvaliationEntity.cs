using TrainMaster.Domain.General;

namespace TrainMaster.Domain.Entity
{
    public class CourseAvaliationEntity : BaseEntity
    {
        public int CourseId { get; set; }

        //[Range(1, 5, ErrorMessage = "A avaliação deve estar entre 1 e 5.")]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime ReviewDate { get; set; }

        // Relacionamento com o curso
        public virtual CourseEntity? Course { get; set; }
    }
}