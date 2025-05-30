﻿using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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
                .OrderBy(course => course.Name)
                .Select(course => new CourseEntity
                {
                    Id = course.Id,
                    Name = course.Name,
                    Description = course.Description,
                    StartDate = course.StartDate,
                    EndDate = course.EndDate,
                    IsActive = course.IsActive,
                })
                .ToListAsync();
        }

        public async Task<CourseEntity?> GetById(int? id)
        {
            return await _context.CourseEntity.FirstOrDefaultAsync(courseEntity => courseEntity.Id == id);
        }

        public async Task<List<CourseEntity>> GetByUserId(int? id)
        {
            return await _context.CourseEntity
                .Where(course => course.UserId == id)
                .ToListAsync();
        }

        public CourseEntity Update(CourseEntity courseEntity)
        {
            var response = _context.CourseEntity.Update(courseEntity);
            return response.Entity;
        }
    }
}
