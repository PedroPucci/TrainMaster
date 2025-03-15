using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/team")]
    public class TeamController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public TeamController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] TeamEntity teamEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.TeamService.Add(teamEntity);
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody] TeamEntity teamEntity)
        {
            var result = await _serviceUoW.TeamService.Update(teamEntity);
            return result.Success ? Ok(result) : BadRequest(teamEntity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceUoW.TeamService.Delete(id);
            return Ok();
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TeamEntity>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _serviceUoW.TeamService.Get();
            return Ok(result);
        }
    }
}