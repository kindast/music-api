using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusicAPI.Dto;
using MusicAPI.Interfaces;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : Controller
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ArtistController(IArtistRepository artistRepository, IUserRepository userRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("/api/artist")]
        [Authorize]
        public IActionResult GetArtist(int id)
        {
            if (!_artistRepository.ArtistExists(id))
                return BadRequest(ModelState);

            string? strId = User.FindFirst("id")?.Value;
            if (strId.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Invalid token");
                return StatusCode(422, ModelState);
            }

            int userId;
            if (!int.TryParse(strId, out userId))
            {
                ModelState.AddModelError("", "Invalid token");
                return StatusCode(423, ModelState);
            }

            if (!_userRepository.UserExists(userId))
            {
                ModelState.AddModelError("", "Invalid token");
                return StatusCode(424, ModelState);
            }

            var artist = _mapper.Map<ArtistDetailsDto>(_artistRepository.GetArtist(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(artist);
        }
    }
}
