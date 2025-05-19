//using Microsoft.AspNetCore.Mvc;
//using TrainMaster.Application.UnitOfWork;
//using TrainMaster.Domain.Entity;

//namespace TrainMaster.Controllers
//{
//    [ApiController]
//    [Route("api/v1/loginSystem")]
//    public class LoginController : Controller
//    {
//        private readonly IUnitOfWorkService _serviceUoW;

//        public LoginController(IUnitOfWorkService unitOfWorkService)
//        {
//            _serviceUoW = unitOfWorkService;
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> DoLogin([FromBody] LoginEntity loginEntity)
//        {            
//            var result = await _serviceUoW.AuthService.Login(loginEntity.Cpf, loginEntity.Password);

//            if (!result.Success)
//                return Unauthorized(new { message = result.Message });

//            return Ok(new { accessToken = result.Data });
//        }
//    }
//}
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

        //[HttpPost]
        //public async Task<IActionResult> Index(LoginDto login)
        //{
        //    if (!ModelState.IsValid)
        //        return View(login);

        //    var result = await _unitOfWork.AuthService.Login(login.Cpf, login.Password);
        //    if (!result.Success)
        //    {
        //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //        return View(login);
        //    }

        //    HttpContext.Session.SetString("UserId", result.Data.Cpf);
        //    return RedirectToAction("Index", "Home");
        //}

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

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // limpa todos os dados da sessão
            return RedirectToAction("Index", "Login");
        }
    }
}