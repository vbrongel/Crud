using Crud.Application.Interface;
using Crud.Core.Entities;
using Crud.API.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Crud.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class ControllerUser : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public ControllerUser(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] UserDto userDto)
        {
            try
            {
                await _service.Add(_mapper.Map<User>(userDto));
                return Ok("Usuário inserido");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var recordsUser = await _service.GetAll();
                return Ok(_mapper.Map<List<UserDto>>(recordsUser));
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);   
            }
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] UserDto userDto)
        {
            try
            {
                await _service.Edit(_mapper.Map<User>(userDto));
                return Ok("Edição realizada");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove([FromBody] string email) 
        {
            try
            {
                await _service.RemoveByEmail(email);
                return Ok("Usuário removido");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
