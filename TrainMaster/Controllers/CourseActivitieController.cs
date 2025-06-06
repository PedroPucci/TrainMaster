using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;
using TrainMaster.Domain.ValueObject;

namespace TrainMaster.Controllers
{
    [Route("Atividades")]
    public class CourseActivitieController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWork;

        public CourseActivitieController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var courses = await _unitOfWork.CourseService.Get();
            ViewBag.Courses = courses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View("Create");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CourseActivitieEntity entity)
        {
            if (!ModelState.IsValid)
            {
                await LoadCoursesAsync();
                ViewBag.ErrorMessage = "Todos os campos devem ser preenchidos.";
                return View("Create", entity);
            }

            var result = await _unitOfWork.CourseActivitieService.Add(entity);

            if (!result.Success)
            {
                await LoadCoursesAsync();
                ViewBag.ErrorMessage = "Erro ao registrar atividade.";
                return View("Create", entity);
            }

            return RedirectToAction("Index");
        }

        private async Task LoadCoursesAsync()
        {
            var courses = await _unitOfWork.CourseService.Get();
            ViewBag.Courses = courses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
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
            var courses = await _unitOfWork.CourseService.Get();
            ViewBag.Courses = courses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

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

            existing.Name = model.Name;
            existing.Description = model.Description;
            //existing.StartDate = model.StartDate;
            //existing.DueDate = model.DueDate;
            var startUtc = DateTime.SpecifyKind(model.Period.StartDate, DateTimeKind.Utc);
            var dueUtc = DateTime.SpecifyKind(model.Period.EndDate, DateTimeKind.Utc);
            existing.SetPeriod(new Period(startUtc, dueUtc));
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