using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Dto;

namespace TrainMaster.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWork;

        public LoginController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Os campos CPF e senha estão incorretos.");
                return View(login);
            }

            var result = await _unitOfWork.AuthService.Login(login.Cpf, login.Password);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, "Os campos CPF e senha estão incorretos.");
                return View(login);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        [HttpGet()]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("SendRecovery")]
        public async Task<IActionResult> SendRecovery(ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return View("ForgotPassword", dto);

            var result = await _unitOfWork.AuthService.ResetPassword(dto.Email);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View("ForgotPassword", dto);
            }

            ViewBag.NovaSenha = result.Data;
            return View("ForgotPassword", dto);
        }
    }
}