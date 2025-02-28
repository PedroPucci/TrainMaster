using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/address")]
    public class AddressController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public AddressController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpGet("{postalCode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string postalCode)
        {
            var address = await _serviceUoW.AddressService.FindAddressByZipCode(postalCode);
            return Ok(address);
        }
    }
}