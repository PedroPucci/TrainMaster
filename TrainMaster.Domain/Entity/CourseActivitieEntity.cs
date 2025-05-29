using TrainMaster.Domain.General;

namespace TrainMaster.Domain.Entity
{
    public class CourseActivitieEntity : BaseEntity
    {        
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int MaxScore { get; set; }

        public int CourseId { get; set; }
        public virtual CourseEntity? Course { get; set; }
    }
}
