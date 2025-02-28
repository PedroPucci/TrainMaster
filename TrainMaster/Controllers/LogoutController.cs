using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/logout")]
    public class LogoutController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public LogoutController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpPost("logout")]
        public async Task<IActionResult> DoLogin([FromBody] LoginEntity loginEntity)
        {
            if (string.IsNullOrWhiteSpace(loginEntity.Cpf) || string.IsNullOrWhiteSpace(loginEntity.Password))
                return BadRequest(new { message = "CPF and password are required." });

            var result = await _serviceUoW.AuthService.Login(loginEntity.Cpf, loginEntity.Password);

            if (!result.Success)
            {
                return Unauthorized(new { message = result.Message });
            }

            return Ok(new { accessToken = result.Data });
        }
    }
}
