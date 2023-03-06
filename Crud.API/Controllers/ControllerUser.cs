using Crud.Application.Interface;
using Crud.Core.Entities;
using Crud.API.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Crud.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class ControllerUser : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<User> _logger;

        public ControllerUser(IUserService service, IMapper mapper, ILogger<User> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] UserDto userDto)
        {
            try
            {
                _logger.LogInformation($"Inserindo novo usuário: {userDto.Name}. {DateTime.Now}");
                return Ok(await _service.Add(_mapper.Map<User>(userDto)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation($"Selecionando todos os usuários. {DateTime.Now}");
                var recordsUser = await _service.GetAll();
                return Ok(_mapper.Map<List<UserDto>>(recordsUser));
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);   
            }
        }
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("GetByEmail")]
        public async Task<IActionResult> GetByEmail([FromBody] UserDtoSelectOrDelete userDtoSelect)
        {
            try
            {
                _logger.LogInformation($"Selecionando usuário pelo email {userDtoSelect.Email}");
                var user =  await _service.GetByEmail(userDtoSelect.Email);
                if (user == null)
                    return Ok("Usuario não encontrado!");
                return Ok(_mapper.Map<UserDto>(user));
            }
            catch(Exception e) 
            {
                return StatusCode(500, e.Message);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] UserDtoUpdate userDto)
        {
            try
            {
                _logger.LogInformation($"Atualizando usuário {userDto.Name}. {DateTime.Now}");
                return Ok(await _service.Edit(_mapper.Map<User>(userDto)));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove([FromBody] UserDtoSelectOrDelete userDto) 
        {
            try
            {
                _logger.LogInformation($"Deletendo usuário {userDto.Email}. {DateTime.Now}");
                await _service.RemoveByEmail(userDto.Email);
                return Ok("Usuário removido");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
