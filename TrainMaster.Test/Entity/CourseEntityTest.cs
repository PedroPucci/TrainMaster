using TrainMaster.Domain.Entity;
using TrainMaster.Domain.ValueObject;

namespace TrainMaster.Test.Entities
{
    public class CourseEntityTest
    {
        [Fact]
        public void Should_Create_CourseEntity_With_Valid_Data()
        {
            var name = "Curso de Teste";
            var description = "Descrição do curso";
            var startDate = new DateTime(2025, 6, 1);
            var endDate = new DateTime(2025, 7, 1);
            var isActive = true;

            var period = new Period(startDate, endDate);

            var course = new CourseEntity
            {
                Name = name,
                Description = description,
                Period = period,
                IsActive = isActive
            };

            Assert.Equal(name, course.Name);
            Assert.Equal(description, course.Description);
            Assert.Equal(startDate, course.Period.StartDate);
            Assert.Equal(endDate, course.Period.EndDate);
            Assert.True(course.IsActive);
        }

        [Fact]
        public void Should_Create_CourseEntity_With_Default_Values()
        {
            var course = new CourseEntity();

            Assert.Null(course.Name);
            Assert.Null(course.Description);
            Assert.Null(course.Period);
            Assert.False(course.IsActive);
        }
    }
}
