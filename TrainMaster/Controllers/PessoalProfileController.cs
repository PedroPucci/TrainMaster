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
        var userId = HttpContext.Session.GetString("UserId");
        ViewBag.UserId = userId;
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
    public async Task<IActionResult> Edit()
    {
        var userId = HttpContext.Session.GetString("UserId");
        ViewBag.UserId = userId;

        int id = Convert.ToInt32(userId);

        var result = await _serviceUoW.ProfilePessoalService.GetById(id);

        var model = result?.Data ?? new PessoalProfileEntity();

        ViewBag.UserId = id;

        return View("~/Views/Perfil/Pessoal.cshtml", model);
    }


    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(int id, ProfessionalProfileEntity model)
    {
        if (!ModelState.IsValid)
            return View("~/Views/PerfilProfessional/Professional.cshtml", model);

        var result = await _serviceUoW.ProfileProfessionalService.Update(id, model);

        if (!result.Success)
        {
            ViewBag.ErrorMessage = result.Message;
            return View("~/Views/PerfilProfessional/Professional.cshtml", model);
        }

        ViewBag.Sucesso = "Perfil profissional atualizado com sucesso!";
        return View("~/Views/PerfilProfessional/Professional.cshtml", model);
    }
}