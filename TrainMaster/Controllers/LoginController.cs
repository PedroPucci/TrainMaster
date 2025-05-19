using Microsoft.AspNetCore.Mvc;
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
                ModelState.AddModelError(string.Empty, "Os campos CPF e email estão incorretos.");
                return View(login);
            }

            var result = await _unitOfWork.AuthService.Login(login.Cpf, login.Password);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, "Os campos CPF e email estão incorretos.");
                return View(login);
            }

            HttpContext.Session.SetString("UserId", result.Data.Cpf.ToString());
            return RedirectToAction("Index", "Home");
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

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}