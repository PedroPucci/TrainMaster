using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/avaliations")]
    public class CourseAvaliationController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;

        public CourseAvaliationController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var avaliacoes = await _unitOfWork.CourseAvaliationService.GetAll();
            return Ok(avaliacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var avaliacao = await _unitOfWork.CourseAvaliationService.GetById(id);
            return avaliacao == null ? NotFound("Avaliação não encontrada.") : Ok(avaliacao);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseAvaliationEntity entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.CourseAvaliationService.Add(entity);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity)
                : BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseAvaliationEntity model)
        {
            if (id != model.Id)
                return BadRequest("ID da URL não corresponde ao corpo da requisição.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _unitOfWork.CourseAvaliationService.GetById(id);
            if (existing == null)
                return NotFound("Avaliação não encontrada.");

            existing.CourseId = model.CourseId;
            existing.Rating = model.Rating;
            existing.Comment = model.Comment;
            existing.ReviewDate = model.ReviewDate;

            var result = await _unitOfWork.CourseAvaliationService.Update(existing);
            return result.Success ? Ok(existing) : BadRequest(result.Message);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var existing = await _unitOfWork.CourseAvaliationService.GetById(id);
        //    if (existing == null)
        //        return NotFound("Avaliação não encontrada.");

        //    var result = await _unitOfWork.CourseAvaliationService.Delete(id);
        //    return result.Success ? NoContent() : BadRequest(result.Message);
        //}
    }
}