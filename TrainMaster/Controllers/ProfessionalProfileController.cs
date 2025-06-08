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
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "Usuário não autenticado." });

            int userId = Convert.ToInt32(userIdClaim);
            model.UserId = userId;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.ProfileProfessionalService.Update(userId, model);
            return result.Success
                ? Ok(new { message = "Perfil profissional atualizado com sucesso.", data = model })
                : BadRequest(new { message = result.Message });
        }
    }
}