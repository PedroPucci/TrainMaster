using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [Route("Departamentos")]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public DepartmentController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var departamentos = await _serviceUoW.DepartmentService.Get();
            var totalDepartamentos = departamentos.Count();
            var departamentosPaginados = departamentos
                .OrderBy(d => d.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalDepartamentos / pageSize);
            //ViewBag.TotalCursos = totalDepartamentos;

            return View("Index", departamentosPaginados);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetString("UserId");
            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(DepartmentEntity department)
        {
            if (!ModelState.IsValid)
                return View(department);

            var result = await _serviceUoW.DepartmentService.Add(department);
            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View(department);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _serviceUoW.DepartmentService.GetById(id);
            if (result?.Data == null)
                return NotFound();

            ModelState.Clear();
            return View("~/Views/Department/Edit.cshtml", result.Data);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, DepartmentEntity department)
        {
            if (id != department.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(department);

            var result = await _serviceUoW.DepartmentService.Update(department);
            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View(department);
            }

            return RedirectToAction("Index");
        }
    }
}
