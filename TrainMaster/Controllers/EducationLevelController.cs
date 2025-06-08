using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/education")]
    public class EducationLevelController : ControllerBase
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public EducationLevelController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _serviceUoW.EducationLevelService.Get();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _serviceUoW.EducationLevelService.GetById(id);
            return result?.Data == null
                ? NotFound("Nível de educação não encontrado.")
                : Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EducationLevelEntity model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.EducationLevelService.Add(model);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = model.Id }, model)
                : BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EducationLevelEntity model)
        {
            if (id != model.Id)
                return BadRequest("ID da URL não corresponde ao corpo da requisição.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.EducationLevelService.Update(id, model);
            return result.Success ? Ok(model) : BadRequest(result.Message);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _serviceUoW.EducationLevelService.Delete(id);
        //    return result.Success ? NoContent() : BadRequest(result.Message);
        //}
    }
}