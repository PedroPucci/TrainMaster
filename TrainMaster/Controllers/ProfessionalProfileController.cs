using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/professional-profile")]
    public class ProfessionalProfileController : ControllerBase
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public ProfessionalProfileController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "Usuário não autenticado." });

            int userId = Convert.ToInt32(userIdClaim);
            var result = await _serviceUoW.ProfileProfessionalService.GetById(userId);

            return result?.Data == null
                ? NotFound("Perfil profissional não encontrado.")
                : Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfessionalProfileEntity model)
        {
            //var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (string.IsNullOrEmpty(userIdClaim))
            //    return Unauthorized(new { message = "Usuário não autenticado." });

            //int userId = Convert.ToInt32(userIdClaim);
            //model.UserId = userId;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.ProfileProfessionalService.Update(model.UserId, model);
            return result.Success
                ? Ok(new { message = "Perfil profissional atualizado com sucesso.", data = model })
                : BadRequest(new { message = result.Message });
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ProfessionalProfileEntity model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.ProfileProfessionalService.Add(model);

            return result.Success
                ? Ok(new { message = "Perfil profissional criado com sucesso.", data = result.Data })
                : BadRequest(new { message = result.Message });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _serviceUoW.ProfileProfessionalService.Delete(id);
                return Ok(new { message = "Perfil profissional excluído com sucesso." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _serviceUoW.ProfileProfessionalService.Get();
                return Ok(list);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}