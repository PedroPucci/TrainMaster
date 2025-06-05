using TrainMaster.Domain.General;
using TrainMaster.Domain.ValueObject;

namespace TrainMaster.Domain.Entity
{
    public class CourseActivitieEntity : BaseEntity
    {        
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Period Period { get; set; }
        public int MaxScore { get; set; }

        public int CourseId { get; set; }
        public virtual CourseEntity? Course { get; set; }

        public void SetPeriod(Period period)
        {
            Period = period;
        }
    }
}