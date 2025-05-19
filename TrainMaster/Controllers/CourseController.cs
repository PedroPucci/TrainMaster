using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Dto;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [Route("Cursos")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public CourseController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var cursos = await _serviceUoW.CourseService.Get();
            var totalCursos = cursos.Count();
            var cursosPaginados = cursos
                .OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCursos / pageSize);

            return View("Index", cursosPaginados);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CourseDto course)
        {
            if (!ModelState.IsValid)
                return View(course);

            var result = await _serviceUoW.CourseService.Add(course);
            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View(course);
            }

            return RedirectToAction("Index");
        }

        //[HttpGet("Edit/{id}")]
        //public async Task<IActionResult> Edit(CourseEntity course)
        //{
        //    var result = await _serviceUoW.CourseService.Update(course);
        //    if (result?.Data == null)
        //        return NotFound();

        //    return View(result.Data);
        //}

        //[HttpPost("Edit/{id}")]
        //public async Task<IActionResult> Edit(int id, CourseEntity course)
        //{
        //    if (id != course.Id || !ModelState.IsValid)
        //        return View(course);

        //    var result = await _serviceUoW.CourseService.Update(course);
        //    if (!result.Success)
        //    {
        //        ViewBag.ErrorMessage = result.Message;
        //        return View(course);
        //    }

        //    return RedirectToAction("Index");
        //}

        //[HttpPost("Delete/{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _serviceUoW.CourseService.Delete(id);
        //    return RedirectToAction("Index");
        //}
    }
}