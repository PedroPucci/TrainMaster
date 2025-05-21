using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [Route("Endereco")]
    public class AddressController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public AddressController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }


        [HttpGet("GetByPostalCode")]
        public async Task<IActionResult> GetByPostalCode(string postalCode)
        {
            if (string.IsNullOrEmpty(postalCode))
                return BadRequest("CEP inválido.");

            var result = await _serviceUoW.AddressService.GetAddressByZipCode(postalCode);
            if (result.Success)
                return Json(result.Data);

            return NotFound(result.Message);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _serviceUoW.AddressService.GetById(id);
            if (result?.Data == null)
                return NotFound();

            return View("~/Views/Address/Edit.cshtml", result.Data);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, AddressEntity model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var result = await _serviceUoW.AddressService.Update(id, model);
            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View(model);
            }

            ViewBag.Sucesso = "Endereço atualizado com sucesso!";
            return View("~/Views/Address/Edit.cshtml", model);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            var model = new AddressEntity();
            return View("~/Views/Address/Create.cshtml", model);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(AddressEntity model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _serviceUoW.AddressService.Add(model);
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
            var result = await _serviceUoW.AddressService.Get();
            return View("~/Views/Address/List.cshtml", result);
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceUoW.AddressService.Delete(id);
            return RedirectToAction("List");
        }
    }
}