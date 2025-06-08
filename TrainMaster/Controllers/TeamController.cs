using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Domain.Entity;

namespace TrainMaster.Controllers
{
    [ApiController]
    [Route("api/v1/teams")]
    public class TeamController : ControllerBase
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public TeamController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeams([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "Usuário não autenticado." });

            var userId = Convert.ToInt32(userIdClaim);
            var times = await _serviceUoW.TeamService.GetByUserId(userId);

            var total = times.Count();
            var paged = times
                .OrderBy(t => t.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new
            {
                currentPage = page,
                totalPages = (int)Math.Ceiling((double)total / pageSize),
                totalItems = total,
                data = paged
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeamEntity team)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "Usuário não autenticado." });

            int userId = Convert.ToInt32(userIdClaim);
            var departmentResult = await _serviceUoW.DepartmentService.GetByUserId(userId);

            if (departmentResult?.Data == null)
                return BadRequest(new { message = "Departamento do usuário não encontrado." });

            team.DepartmentId = departmentResult.Data.Id;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.TeamService.Add(team);
            return result.Success
                ? Ok(new { message = "Time criado com sucesso!", data = team })
                : BadRequest(new { message = result.Message });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _serviceUoW.TeamService.GetById(id);
            return result?.Data == null
                ? NotFound(new { message = "Time não encontrado." })
                : Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeamEntity team)
        {
            if (id != team.Id)
                return BadRequest(new { message = "ID da URL não confere com o corpo da requisição." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.TeamService.Update(team);
            return result.Success
                ? Ok(new { message = "Time atualizado com sucesso!", data = team })
                : BadRequest(new { message = result.Message });
        }
    }
}