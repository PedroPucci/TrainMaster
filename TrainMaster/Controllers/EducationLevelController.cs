using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [Route("Educacao")]
    public class EducationLevelController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public EducationLevelController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _serviceUoW.EducationLevelService.GetById(id);
            var model = result?.Data ?? new EducationLevelEntity();

            return View("~/Views/Education/Edit.cshtml", model);
        }


        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, EducationLevelEntity model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var result = await _serviceUoW.EducationLevelService.Update(model);
            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View(model);
            }

            ViewBag.Sucesso = "Seus dados de educação foram atualizados com sucesso!";
            return View("~/Views/Education/Edit.cshtml", model);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            var model = new EducationLevelEntity();
            return View("~/Views/Education/Create.cshtml", model);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(EducationLevelEntity model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _serviceUoW.EducationLevelService.Add(model);
            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View(model);
            }

            return RedirectToAction("List");
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var result = await _serviceUoW.EducationLevelService.Get();
            return View("~/Views/Education/List.cshtml", result);
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceUoW.EducationLevelService.Delete(id);
            return RedirectToAction("List");
        }
    }
}