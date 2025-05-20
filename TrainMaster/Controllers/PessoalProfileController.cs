using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;
using TrainMaster.Domain.Enums;

[Route("perfil")]
public class PerfilController : Controller
{
    private readonly IUnitOfWorkService _serviceUoW;

    public PerfilController(IUnitOfWorkService unitOfWorkService)
    {
        _serviceUoW = unitOfWorkService;
    }

    [HttpGet("Pessoal")]
    public IActionResult Pessoal()
    {
        var model = new PessoalProfileEntity();
        return View("~/Views/Perfil/Pessoal.cshtml", model);
    }

    [HttpPost("Pessoal")]
    public IActionResult Pessoal(PessoalProfileEntity model)
    {
        ViewBag.Sucesso = "Perfil salvo com sucesso!";
        return View("Pessoal", model);
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _serviceUoW.ProfilePessoalService.GetById(id);
        if (result?.Data == null)
            return NotFound();

        ModelState.Clear();
        return View("~/Views/Perfil/Pessoal.cshtml", result.Data);
    }

    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(int id, PessoalProfileEntity course)
    {
        if (id != course.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(course);

        var result = await _serviceUoW.ProfilePessoalService.Update(id, course);
        if (!result.Success)
        {
            ViewBag.ErrorMessage = result.Message;
            return View(course);
        }

        return RedirectToAction("Edit", new { id = course.Id });
    }
}