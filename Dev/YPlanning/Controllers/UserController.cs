using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YPlanning.Models;
using YPlanning.Interfaces.Services;
using YPlanning.Dto;

namespace YPlanning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        public IActionResult GetUsers() 
        {
            var users = _userService.GetUsers();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            
            return Ok(usersDto);
        }

        [HttpGet("{userId:int}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(int? userId)
        {
            if (userId == null)
                return BadRequest("User ID cannot be null");

            if (!_userService.DoesUserExistById(userId))
                return NotFound();

            var user = _userService.GetUserById(userId);
            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpGet("{lastName}/{firstName}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUserByName(string? lastName, string? firstName)
        {
            if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName))
                return BadRequest("Last name / First name cannot be null or empty");

            if (!_userService.DoesUserExistByName(lastName, firstName))
                return NotFound();

            var user = _userService.GetUserByName(lastName, firstName);
            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }
        
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest("User cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_userService.DoesUserDtoExists(userCreate))
            {
                ModelState.AddModelError("", "User already exists");
                return Conflict(ModelState);
            }

            userCreate.BirthDate = userCreate.BirthDate?.ToUniversalTime();
            
            var userMap = _mapper.Map<User>(userCreate);
            if (!_userService.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            
            return Ok("User successfully created");
        }

        [HttpPut("{userId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateUser(int? userId, [FromBody] UserDto updatedUser)
        {
            if (userId == null)
                return BadRequest("User ID cannot be null");

            if (updatedUser == null)
                return BadRequest("User cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userService.DoesUserExistById(userId))
                return NotFound();
            
            updatedUser.BirthDate = updatedUser.BirthDate?.ToUniversalTime();

            var userMap = _mapper.Map<User>(updatedUser);
            userMap.Id = userId ?? -1;

            if (!_userService.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{userId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUserById(int? userId)
        {
            if (userId == null)
                return BadRequest("User ID cannot be null");

            if (!_userService.DoesUserExistById(userId))
                return NotFound();

            if (!_userService.DeleteUserById(userId))
            {
                ModelState.AddModelError("", "Something went wrong deleting the user");
                return StatusCode(500, ModelState);
            }
            
            return NoContent();
        }

        [HttpDelete("{lastName}/{firstName}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUserByName(string? lastName, string? firstName)
        {
            if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName))
                return BadRequest("Last name / First name cannot be null or empty");

            if (!_userService.DoesUserExistByName(lastName, firstName))
                return NotFound();
            
            if (!_userService.DeleteUserByName(lastName, firstName))
            {
                ModelState.AddModelError("", "Something went wrong deleting the user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
