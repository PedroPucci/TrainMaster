using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/profile")]
    public class PerfilController : ControllerBase
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public PerfilController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpGet("personal")]
        public async Task<IActionResult> GetPersonalProfile()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "Usuário não autenticado." });

            int userId = Convert.ToInt32(userIdClaim);

            var result = await _serviceUoW.ProfilePessoalService.GetById(userId);
            return result?.Data == null
                ? NotFound("Perfil não encontrado.")
                : Ok(result.Data);
        }

        [HttpPost("personal")]
        public async Task<IActionResult> SavePersonalProfile([FromBody] PessoalProfileEntity model)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "Usuário não autenticado." });

            int userId = Convert.ToInt32(userIdClaim);
            model.Id = userId;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.ProfilePessoalService.Update(userId, model);
            return result.Success
                ? Ok(new { message = "Perfil atualizado com sucesso.", data = model })
                : BadRequest(new { message = result.Message });
        }
    }
}