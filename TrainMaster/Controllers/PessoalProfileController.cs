using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

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
}