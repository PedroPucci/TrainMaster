using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public DepartmentController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpGet("by-user")]
        public async Task<IActionResult> GetByUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Usuário não autenticado.");

            var result = await _serviceUoW.DepartmentService.GetByUserId(Convert.ToInt32(userId));
            return !result.Success || result.Data == null ? NotFound(result.Message) : Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _serviceUoW.DepartmentService.GetById(id);
            return result?.Data == null ? NotFound("Departamento não encontrado.") : Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentEntity department)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.DepartmentService.Add(department);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = department.Id }, department)
                : BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentEntity department)
        {
            if (id != department.Id)
                return BadRequest("ID da URL não corresponde ao corpo da requisição.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.DepartmentService.Update(department);
            return result.Success ? Ok(department) : BadRequest(result.Message);
        }
    }
}