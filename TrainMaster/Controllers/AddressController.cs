using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/addresses")]
    public class AddressController : ControllerBase
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public AddressController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpGet("by-postal-code/{postalCode}")]
        public async Task<IActionResult> GetByPostalCode(string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                return BadRequest("CEP inválido.");

            var result = await _serviceUoW.AddressService.GetAddressByZipCode(postalCode);
            return result.Success ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _serviceUoW.AddressService.GetById(id);
            return result.Data == null ? NotFound() : Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddressEntity model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.AddressService.Add(model);
            return result.Success ? CreatedAtAction(nameof(GetById), new { id = model.Id }, model)
                                  : BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AddressEntity model)
        {
            if (id != model.Id)
                return BadRequest("ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.AddressService.Update(id, model);
            return result.Success ? Ok(model) : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _serviceUoW.AddressService.Get();
            return Ok(result);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _serviceUoW.AddressService.Delete(id);
        //    return result.Success ? NoContent() : BadRequest(result.Message);
        //}
    }
}