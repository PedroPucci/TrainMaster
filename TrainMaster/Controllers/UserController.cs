using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Dto;
using TrainMaster.Domain.Entity;
using TrainMaster.Infrastracture.Security.Cryptography;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;

        public UserController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateUpdateDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new UserEntity
            {
                Email = userDto.Email,
                Cpf = userDto.Cpf,
                Password = new BCryptoAlgorithm().HashPassword(userDto.Password)
            };

            var result = await _unitOfWork.UserService.Add(user);

            return result.Success
                ? Ok(new { message = "Usuário registrado com sucesso.", userId = user.Id })
                : BadRequest(new { message = "Erro ao registrar usuário.", error = result.Message });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _unitOfWork.UserService.GetById(id);
            return user?.Data == null
                ? NotFound(new { message = "Usuário não encontrado." })
                : Ok(user.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserEntity model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _unitOfWork.UserService.GetById(id);
            if (existing?.Data == null)
                return NotFound(new { message = "Usuário não encontrado." });

            existing.Data.Cpf = !string.IsNullOrWhiteSpace(model.Cpf) ? model.Cpf : existing.Data.Cpf;
            existing.Data.Email = !string.IsNullOrWhiteSpace(model.Email) ? model.Email : existing.Data.Email;

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                var crypto = new BCryptoAlgorithm();
                existing.Data.Password = crypto.HashPassword(model.Password);
            }

            var dto = new UserUpdateDto
            {
                Id = existing.Data.Id,
                Cpf = existing.Data.Cpf,
                Email = existing.Data.Email,
                Password = existing.Data.Password
            };

            var result = await _unitOfWork.UserService.Update(dto);
            return result.Success
                ? Ok(new { message = "Usuário atualizado com sucesso.", data = dto })
                : BadRequest(new { message = "Erro ao atualizar usuário.", error = result.Message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _unitOfWork.UserService.GetById(id);
            if (user?.Data == null)
                return NotFound(new { message = "Usuário não encontrado." });

            try
            {
                await _unitOfWork.UserService.Delete(id);
                return Ok(new { message = "Usuário deletado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao deletar usuário.", error = ex.Message });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _unitOfWork.UserService.Get();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao carregar usuários.", error = ex.Message });
            }
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActives()
        {
            try
            {
                var users = await _unitOfWork.UserService.GetAllActives();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao carregar usuários ativos.", error = ex.Message });
            }
        }
    }
}