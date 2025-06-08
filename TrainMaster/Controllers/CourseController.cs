using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Dto;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/courses")]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public CourseController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpGet("by-user")]
        public async Task<IActionResult> GetByUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Usuário não autenticado.");

            var cursos = await _serviceUoW.CourseService.GetByUserId(int.Parse(userId));
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _serviceUoW.CourseService.GetById(id);
            return result?.Data == null ? NotFound("Curso não encontrado.") : Ok(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CourseDto course)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.CourseService.Add(course);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseEntity course)
        {
            if (id != course.Id)
                return BadRequest("ID da URL não corresponde ao corpo da requisição.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.CourseService.Update(course);
            return result.Success ? Ok(course) : BadRequest(result.Message);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _serviceUoW.CourseService.Delete(id);
        //    return result.Success ? NoContent() : BadRequest(result.Message);
        //}
    }
}