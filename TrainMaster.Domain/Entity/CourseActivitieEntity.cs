using TrainMaster.Domain.General;

namespace TrainMaster.Domain.Entity
{
    public class CourseActivitieEntity : BaseEntity
    {        
        //[Required]
        //[MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        //[DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        //[DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        //[Range(0, int.MaxValue)]
        public int MaxScore { get; set; }

        public int CourseId { get; set; }
        public virtual CourseEntity? Course { get; set; }
    }
}
