using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/activities")]
    public class CourseActivitieController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;

        public CourseActivitieController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var activities = await _unitOfWork.CourseActivitieService.GetAll();
            return Ok(activities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var activity = await _unitOfWork.CourseActivitieService.GetById(id);
            if (activity == null)
                return NotFound("Atividade não encontrada.");

            return Ok(activity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseActivitieEntity entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.CourseActivitieService.Add(entity);

            if (!result.Success)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseActivitieEntity model)
        {
            if (id != model.Id)
                return BadRequest("ID da URL não corresponde ao corpo da requisição.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _unitOfWork.CourseActivitieService.GetById(id);
            if (existing == null)
                return NotFound("Atividade não encontrada.");

            existing.Title = model.Title;
            existing.Description = model.Description;
            existing.StartDate = model.StartDate;
            existing.DueDate = model.DueDate;
            existing.MaxScore = model.MaxScore;
            existing.CourseId = model.CourseId;

            var result = await _unitOfWork.CourseActivitieService.Update(existing);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(existing);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var existing = await _unitOfWork.CourseActivitieService.GetById(id);
        //    if (existing == null)
        //        return NotFound("Atividade não encontrada.");

        //    var result = await _unitOfWork.CourseActivitieService.Delete(id);
        //    return result.Success ? NoContent() : BadRequest(result.Message);
        //}
    }
}