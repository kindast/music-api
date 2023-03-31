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
    public class SearchController : Controller
    {
        private readonly ISongRepository _songRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public SearchController
            (ISongRepository songRepository,
            IAlbumRepository albumRepository, 
            IArtistRepository artistRepository,
            IUserRepository userRepository, 
            IMapper mapper)
        {
            _songRepository = songRepository;
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("/api/search")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(ICollection<AlbumDto>))]
        public IActionResult Search(int type, string name)
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

            switch (type)
            {
                case 0:
                    return Ok(_mapper.Map<ICollection<SongDto>>(_songRepository.GetSongs(name)));
                case 1:
                    return Ok(_mapper.Map<ICollection<AlbumDto>>(_albumRepository.GetAlbums(name)));
                case 2:
                    return Ok(_mapper.Map<ICollection<ArtistDto>>(_artistRepository.GetArtists(name)));
                default:
                    return BadRequest(ModelState);
            }
        }
    }
}
