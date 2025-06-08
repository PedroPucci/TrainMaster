using Microsoft.AspNetCore.Mvc;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/home")]
    public class HomeController : ControllerBase
    {
        [HttpGet("session-user")]
        public IActionResult GetSessionUser()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "Usuário não autenticado." });

            return Ok(new { UserId = userId });
        }
    }
}