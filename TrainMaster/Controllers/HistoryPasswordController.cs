using Microsoft.AspNetCore.Mvc;
using TrainMaster.Application.UnitOfWork;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/history-password")]
    public class HistoryPasswordController : ControllerBase
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public HistoryPasswordController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        public class UpdatePasswordRequest
        {
            public string NewPassword { get; set; }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOldPassword(int id, [FromBody] UpdatePasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.NewPassword))
                return BadRequest("A nova senha é obrigatória.");

            var result = await _serviceUoW.HistoryPasswordService.UpdateOldPassword(id, request.NewPassword);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}