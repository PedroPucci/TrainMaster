using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/pessoalProfiles")]
    public class PessoalProfileController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public PessoalProfileController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] PessoalProfileEntity pessoalProfileEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.ProfilePessoalService.AddPessoalProfileAsync(pessoalProfileEntity);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody] PessoalProfileEntity pessoalProfileEntity)
        {
            var result = await _serviceUoW.ProfilePessoalService.UpdatePessoalProfileAsync(pessoalProfileEntity);
            return result.Success ? Ok(result) : BadRequest(pessoalProfileEntity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceUoW.ProfilePessoalService.DeletePessoalProfileAsync(id);
            return Ok();
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PessoalProfileEntity>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _serviceUoW.ProfilePessoalService.GetAllPessoalProfilesAsync();
            return Ok(users);
        }
    }
}
