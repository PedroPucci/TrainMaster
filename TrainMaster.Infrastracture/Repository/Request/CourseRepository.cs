using Microsoft.EntityFrameworkCore;
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

        public async Task<CourseEntity> Add(CourseEntity courseEntity)
        {
            var result = await _context.CourseEntity.AddAsync(courseEntity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public CourseEntity Delete(CourseEntity CourseEntity)
        {
            var response = _context.CourseEntity.Remove(CourseEntity);
            return response.Entity;
        }

        public async Task<List<CourseEntity>> Get()
        {
            return await _context.CourseEntity
             .OrderBy(course => course.Id)
             .Select(course => new CourseEntity
             {
                 Id = course.Id,
                 Name = course.Name,
                 Description = course.Description,
             }).ToListAsync();
        }

        public async Task<CourseEntity?> GetById(int? id)
        {
            return await _context.CourseEntity.FirstOrDefaultAsync(courseEntity => courseEntity.Id == id);
        }

        public CourseEntity Update(CourseEntity courseEntity)
        {
            var response = _context.CourseEntity.Update(courseEntity);
            return response.Entity;
        }
    }
}
