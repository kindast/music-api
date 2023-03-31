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
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;

        public AlbumController(
            IAlbumRepository albumRepository,
            IUserRepository userRepository,
            ISongRepository songRepository,
            IMapper mapper
            )
        {
            _albumRepository = albumRepository;
            _userRepository = userRepository;
            _songRepository = songRepository;
            _mapper = mapper;
        }

        [HttpGet("/api/album")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(AlbumDetailsDto))]
        public IActionResult GetAlbum(int id)
        {
            if (!_albumRepository.AlbumExists(id))
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

            var album = _mapper.Map<AlbumDetailsDto>(_albumRepository.GetAlbum(id));
            album.IsLiked = _albumRepository.AlbumLiked(id, userId);

            foreach (var song in album.Songs)
                song.IsLiked = _songRepository.SongLiked(song.Id, userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(album);
        }

        [HttpGet("/api/liked-albums")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(ICollection<AlbumDto>))]
        public IActionResult GetLikedAlbums()
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

            var likedAlbums = _mapper.Map<ICollection<AlbumDto>>(_albumRepository.GetLikedAlbums(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(likedAlbums);
        }

        [HttpGet("/api/like-album")]
        [Authorize]
        public IActionResult ToggleLikedAlbum(int id)
        {
            if (!_albumRepository.AlbumExists(id))
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

            if (!_albumRepository.ToggleLikedAlbum(id, userId))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully liked/unliked");
        }
    }
}
