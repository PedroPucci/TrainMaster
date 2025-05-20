//using Microsoft.AspNetCore.Mvc;
//using TrainMaster.Application.UnitOfWork;
//using TrainMaster.Domain.Dto;
//using TrainMaster.Domain.Entity;

//namespace TrainMaster.Controllers
//{
//    [ApiController]
//    [Route("api/v1/pessoalProfiles")]
//    public class PessoalProfileController : Controller
//    {
//        private readonly IUnitOfWorkService _serviceUoW;

//        public PessoalProfileController(IUnitOfWorkService unitOfWorkService)
//        {
//            _serviceUoW = unitOfWorkService;
//        }

//        [HttpPost()]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public async Task<IActionResult> Add([FromBody] PessoalProfileEntity pessoalProfileEntity)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var result = await _serviceUoW.ProfilePessoalService.Add(pessoalProfileEntity);
//            return result.Success ? Ok(result) : BadRequest(result);
//        }

//        [HttpPut]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesDefaultResponseType]
//        public async Task<IActionResult> Update(int id, [FromBody] PessoalProfileEntity pessoalProfileEntity)
//        {
//            var result = await _serviceUoW.ProfilePessoalService.Update(id, pessoalProfileEntity);
//            return result.Success ? Ok(result) : BadRequest(pessoalProfileEntity);
//        }

//        [HttpDelete("{id}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesDefaultResponseType]
//        public async Task<IActionResult> Delete(int id)
//        {
//            await _serviceUoW.ProfilePessoalService.Delete(id);
//            return Ok();
//        }

//        [HttpGet("All")]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PessoalProfileEntity>))]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public async Task<IActionResult> Get()
//        {
//            var result = await _serviceUoW.ProfilePessoalService.Get();
//            return Ok(result);
//        }
//    }
//}

//using Microsoft.AspNetCore.Mvc;
//using TrainMaster.Domain.Entity;

//namespace TrainMaster.Controllers
//{
//    [Route("pessoalProfiles")]
//    public class PerfilController : Controller
//    {
//        [HttpGet("Pessoal")]
//        public IActionResult Pessoal()
//        {
//            // Inicializa o formulário com modelo vazio
//            var model = new PessoalProfileEntity();
//            return View("Pessoal", model);
//        }

//        [HttpPost("Pessoal")]
//        public IActionResult Pessoal(PessoalProfileEntity model)
//        {
//            // Aqui você pode validar e salvar no banco
//            ViewBag.Sucesso = "Perfil salvo com sucesso!";
//            return View("Pessoal", model);
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using TrainMaster.Domain.Entity;

[Route("perfil")]
public class PerfilController : Controller
{
    [HttpGet("Pessoal")]
    public IActionResult Pessoal()
    {
        // Inicializa o formulário com modelo vazio
        var model = new PessoalProfileEntity();
        //return View("Pessoal", model);  // Aqui ele busca a view Pessoal.cshtml dentro de Views/Perfil
        return View("~/Views/Perfil/Pessoal.cshtml", model);
    }

    [HttpPost("Pessoal")]
    public IActionResult Pessoal(PessoalProfileEntity model)
    {
        // Aqui você pode salvar os dados no banco e retornar o sucesso
        ViewBag.Sucesso = "Perfil salvo com sucesso!";
        return View("Pessoal", model);  // Retorna a mesma view com dados atualizados
    }
}
