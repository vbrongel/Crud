using Crud.Infraestructure.Interface;
using Crud.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Crud.API.Controllers
{
    [ApiController]
    [Route("api/token/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILogRepository _logRepository;
        public TokenController(ITokenService tokenService, ILogRepository logRepository)
        {
            _tokenService = tokenService;
            _logRepository = logRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Generate()
        {
            try
            {
                var token = _tokenService.Generate();
                return Ok(token);
            }
            catch (Exception e)
            {
                await _logRepository.Add($"Erro ao tentar gerar token:{e.Message}");
                return BadRequest("Falha ao gerar o token! Tente mais tarde.");
            }
        }
    }
}
