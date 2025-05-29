using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [Route("atividades")]
    public class CourseActivitieController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWork;

        public CourseActivitieController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CourseActivitieEntity model)
        {
            if (!ModelState.IsValid)
                return View("Create", model);

            var result = await _unitOfWork.CourseActivitieService.Add(model);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, "Erro ao cadastrar atividade.");
                return View("Create", model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var atividades = await _unitOfWork.CourseActivitieService.GetAll();
            return View("Index", atividades);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var atividade = await _unitOfWork.CourseActivitieService.GetById(id);
            if (atividade == null)
                return NotFound();

            return View("Edit", atividade);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, CourseActivitieEntity model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            var existing = await _unitOfWork.CourseActivitieService.GetById(id);
            if (existing == null)
                return NotFound();

            existing.Title = model.Title;
            existing.Description = model.Description;
            existing.StartDate = model.StartDate;
            existing.DueDate = model.DueDate;
            existing.MaxScore = model.MaxScore;

            var result = await _unitOfWork.CourseActivitieService.Update(existing);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, "Erro ao atualizar atividade.");
                return View("Edit", existing);
            }

            ViewBag.Sucesso = "Atividade atualizada com sucesso!";
            return View("Edit", existing);
        }
    }
}
