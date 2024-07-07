using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Dtos;
using WebApi.Service.Interface;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // Get user by id
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var data = await _userService.GetUser(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
    }
}

