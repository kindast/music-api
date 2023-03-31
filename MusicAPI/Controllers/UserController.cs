using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusicAPI.Dto;
using MusicAPI.Helper;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("/api/register")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Register([FromQuery] UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            if (_userRepository.UserExists(userCreate.Username))
            {
                ModelState.AddModelError("", "A user with the same username already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User userMap = _mapper.Map<User>(userCreate);
            userMap.CreatedDate = DateTime.UtcNow;

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully registered");
        }

        [HttpGet("/api/authorize")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Authorize(string username, string password)
        {
            if (!_userRepository.UserExists(username, password))
            {
                ModelState.AddModelError("", "Wrong login or password");
                return StatusCode(422, ModelState);
            }

            User user = _userRepository.GetUser(username);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new { access_token = AuthOptions.GenerateToken(user.Id) });
        }

        [HttpGet("/api/user")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult GetUser()
        {
            string? id = User.FindFirst("id")?.Value;
            if (id.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Invalid token");
                return StatusCode(422, ModelState);
            }

            int userId;
            if (!int.TryParse(id, out userId))
            {
                ModelState.AddModelError("", "Invalid token");
                return StatusCode(423, ModelState);
            }

            if (!_userRepository.UserExists(userId))
            {
                ModelState.AddModelError("", "Invalid token");
                return StatusCode(424, ModelState);
            }

            UserProfileDto user = _mapper.Map<UserProfileDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }
    }
}
