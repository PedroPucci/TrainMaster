﻿using TrainMaster.Domain.Entity;

namespace TrainMaster.Infrastracture.Repository.Interfaces
{
    public interface ICourseRepository
    {
        Task<CourseEntity> Add(CourseEntity courseEntity);
        CourseEntity Update(CourseEntity courseEntity);
        CourseEntity Delete(CourseEntity courseEntity);
        Task<List<CourseEntity>> Get();
        Task<CourseEntity?> GetById(int? id);
        Task<List<CourseEntity>> GetByUserId(int? id);
    }
}