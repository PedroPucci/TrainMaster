using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Dto;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [Route("Times")]
    public class TeamController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public TeamController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var times = await _serviceUoW.TeamService.Get();
            var totalTimes = times.Count();
            var timesPaginados = times
                .OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalTimes / pageSize);

            return View("Index", timesPaginados);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetString("UserId");
            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(int userId, TeamEntity team)
        {
            if (!ModelState.IsValid)
                return View(team);

            var resultId = await _serviceUoW.DepartmentService.GetByUserId(userId);

            if (resultId is not null)
            {
                team.DepartmentId = resultId.Data.Id;
                var result = await _serviceUoW.TeamService.Add(team);
                if (!result.Success)
                {
                    ViewBag.ErrorMessage = result.Message;
                    return View(team);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _serviceUoW.TeamService.GetById(id);
            if (result?.Data == null)
                return NotFound();

            ModelState.Clear();
            return View("~/Views/Team/Edit.cshtml", result.Data);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, TeamEntity team)
        {
            if (id != team.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(team);

            var result = await _serviceUoW.TeamService.Update(team);
            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View(team);
            }

            return RedirectToAction("Index");
        }
    }
}