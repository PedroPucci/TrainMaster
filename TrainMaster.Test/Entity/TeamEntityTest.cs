using TrainMaster.Domain.Entity;
using TrainMaster.Domain.ValueObject;

namespace TrainMaster.Test.Entity
{
    public class TeamEntityTest
    {
        [Fact]
        public void TeamEntity_ShouldSetAndGetPropertiesCorrectly()
        {
            var team = new TeamEntity
            {
                Id = 1,
                Name = new Name("Equipe de Desenvolvimento"),
                Description = "Responsável pela criação de software",
                IsActive = true,
                DepartmentId = 42
            };

            Assert.Equal(1, team.Id);
            Assert.Equal("Equipe de Desenvolvimento", team.Name.Value);
            Assert.Equal("Responsável pela criação de software", team.Description);
            Assert.True(team.IsActive);
            Assert.Equal(42, team.DepartmentId);
        }
    }
}