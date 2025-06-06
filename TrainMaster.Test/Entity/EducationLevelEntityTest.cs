using TrainMaster.Domain.Entity;
using TrainMaster.Domain.ValueObject;

namespace TrainMaster.Test.Entity
{
    public class EducationLevelEntityTest
    {
        [Fact]
        public void EducationLevelEntity_ShouldSetAndGetPropertiesCorrectly()
        {
            // Arrange
            var startDate = new DateTime(2020, 01, 01);
            var endDate = new DateTime(2023, 12, 31);
            var period = new Period(startDate, endDate);

            var entity = new EducationLevelEntity
            {
                Id = 1,
                Name = "Graduação em Engenharia",
                Institution = "Universidade XYZ",
                Period = period,
                ProfessionalProfileId = 10
            };

            // Assert
            Assert.Equal(1, entity.Id);
            Assert.Equal("Graduação em Engenharia", entity.Name);
            Assert.Equal("Universidade XYZ", entity.Institution);
            Assert.Equal(startDate, entity.Period.StartDate);
            Assert.Equal(endDate, entity.Period.EndDate);
            Assert.Equal(10, entity.ProfessionalProfileId);
        }
    }
}
