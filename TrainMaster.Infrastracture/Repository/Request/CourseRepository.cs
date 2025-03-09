using TrainMaster.Domain.Entity;
using TrainMaster.Infrastracture.Connections;
using TrainMaster.Infrastracture.Repository.Interfaces;

namespace TrainMaster.Infrastracture.Repository.Request
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public Task<CourseEntity> Add(CourseEntity courseEntity)
        {
            throw new NotImplementedException();
        }

        public CourseEntity Delete(CourseEntity courseEntity)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public CourseEntity Update(CourseEntity courseEntity)
        {
            throw new NotImplementedException();
        }
    }
}
