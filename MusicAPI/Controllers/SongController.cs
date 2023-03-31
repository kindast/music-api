using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusicAPI.Dto;
using MusicAPI.Interfaces;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : Controller
    {
        private readonly ISongRepository _songRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public SongController(ISongRepository songRepository, IUserRepository userRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("/api/liked-songs")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(ICollection<AlbumDto>))]
        public IActionResult GetLikedSongs()
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

            var likedSongs = _mapper.Map<ICollection<SongDto>>(_songRepository.GetLikedSongs(userId));
            foreach (var song in likedSongs)
                song.IsLiked = true;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(likedSongs);
        }

        [HttpGet("/api/like-song")]
        [Authorize]
        public IActionResult ToggleLikedSong(int id)
        {
            if (!_songRepository.SongExists(id))
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

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_songRepository.ToggleLikedSong(id, userId))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully liked/unliked");
        }
    }
}
