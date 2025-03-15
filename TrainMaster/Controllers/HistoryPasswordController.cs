using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/historyPassword")]
    public class HistoryPasswordController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public HistoryPasswordController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOldPassword(int id, string newPassword)
        {
            var result = await _serviceUoW.HistoryPasswordService.UpdateOldPassword(id, newPassword);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}