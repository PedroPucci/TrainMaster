using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

[Route("PerfilProfessional")]
public class ProfessionalProfileController : Controller
{
    private readonly IUnitOfWorkService _serviceUoW;

    public ProfessionalProfileController(IUnitOfWorkService unitOfWorkService)
    {
        _serviceUoW = unitOfWorkService;
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _serviceUoW.ProfileProfessionalService.GetById(id);
        var model = result?.Data ?? new ProfessionalProfileEntity { UserId = id };
        return View("~/Views/PerfilProfessional/Professional.cshtml", model);
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