using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Dto;
using TrainMaster.Domain.Entity;
using TrainMaster.Infrastracture.Security.Cryptography;

namespace TrainMaster.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWork;

        public UserController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            var userId = HttpContext.Session.GetString("UserId");
            ViewBag.UserId = userId;
            return View("Register");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateUpdateDto userEntity)
        {
            if (!ModelState.IsValid)
                return View("Register", userEntity);

            var entity = new UserEntity
            {
                Email = userEntity.Email,
                Password = userEntity.Password,
                Cpf = userEntity.Cpf
            };

            var result = await _unitOfWork.UserService.Add(entity);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, "Erro ao registrar usuário.");
                return View("Register", userEntity);
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _unitOfWork.UserService.GetById(id);
            if (user?.Data == null)
                return NotFound();

            return View(user.Data);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, UserEntity model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _unitOfWork.UserService.GetById(id);
            if (user?.Data == null)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(model.Cpf))
            {
                user.Data.Cpf = model.Cpf;
            }
            else
            {
                user.Data.Cpf = user.Data.Cpf;
            }

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                user.Data.Email = model.Email;
            }
            else
            {
                user.Data.Email = user.Data.Email;
            }

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                user.Data.Password = model.Password;
            }
            else
            {
                user.Data.Password = user.Data.Password;
            }
            
            if (!string.IsNullOrEmpty(model.Password))
            {
                var crypto = new BCryptoAlgorithm();
                user.Data.Password = crypto.HashPassword(model.Password);
            }

            var userDto = new UserUpdateDto
            {
                Id = user.Data.Id,
                Cpf = user.Data.Cpf,
                Email = user.Data.Email,
                Password = user.Data.Password
            };

            await _unitOfWork.UserService.Update(userDto);

            ViewBag.Sucesso = "Usuário atualizado com sucesso!";
            return View("Edit", user.Data);
        }
    }
}