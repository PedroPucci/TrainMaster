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
        public IActionResult SendRecovery(ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return View("ForgotPassword", dto);

            // TODO: lógica de envio do e-mail de recuperação

            TempData["Message"] = "Se o e-mail estiver correto, você receberá instruções para redefinir sua senha.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}