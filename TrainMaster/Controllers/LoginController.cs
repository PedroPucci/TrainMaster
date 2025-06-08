using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Dto;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;

        public LoginController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
                return BadRequest("CPF e senha são obrigatórios.");

            var result = await _unitOfWork.AuthService.Login(login.Cpf, login.Password);
            if (!result.Success)
                return Unauthorized(new { Message = "CPF ou senha inválidos." });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()),
                // Adicione mais claims se necessário (como roles)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Ok(new { Message = "Login realizado com sucesso", UserId = result.Data.Id });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { Message = "Logout realizado com sucesso" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("O e-mail é obrigatório.");

            var result = await _unitOfWork.AuthService.ResetPassword(dto.Email);
            if (!result.Success)
                return BadRequest(new { Message = result.Message });

            return Ok(new { Message = "Nova senha enviada", NovaSenha = result.Data });
        }
    }
}