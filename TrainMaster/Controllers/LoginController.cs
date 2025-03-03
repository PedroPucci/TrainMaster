using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/loginSystem")]
    public class LoginController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public LoginController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> DoLogin([FromBody] LoginEntity loginEntity)
        {            
            var result = await _serviceUoW.AuthService.Login(loginEntity.Cpf, loginEntity.Password);

            if (!result.Success)
                return Unauthorized(new { message = result.Message });

            return Ok(new { accessToken = result.Data });
        }
    }
}