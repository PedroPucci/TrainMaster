using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [Route("Avaliacoes")]
    public class CourseAvaliationController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWork;

        public CourseAvaliationController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CourseAvaliationEntity entity)
        {
            if (!ModelState.IsValid)
                return View("Create", entity);

            var result = await _unitOfWork.CourseAvaliationService.Add(entity);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, "Erro ao registrar avaliação.");
                return View("Create", entity);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var avaliacoes = await _unitOfWork.CourseAvaliationService.GetAll();
            return View("Index", avaliacoes);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var avaliacao = await _unitOfWork.CourseAvaliationService.GetById(id);
            if (avaliacao == null)
                return NotFound();

            return View("Edit", avaliacao);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, CourseAvaliationEntity model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            var existing = await _unitOfWork.CourseAvaliationService.GetById(id);
            if (existing == null)
                return NotFound();

            existing.Rating = model.Rating;
            existing.Comment = model.Comment;
            existing.ReviewDate = model.ReviewDate;

            var result = await _unitOfWork.CourseAvaliationService.Update(existing);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, "Erro ao atualizar avaliação.");
                return View("Edit", existing);
            }

            ViewBag.Sucesso = "Avaliação atualizada com sucesso!";
            return View("Edit", existing);
        }
    }
}
