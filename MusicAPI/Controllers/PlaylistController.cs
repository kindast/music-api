using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicAPI.Dto;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : Controller
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IMapper _mapper;

        public PlaylistController(IPlaylistRepository playlistRepository, IMapper mapper)
        {
            _playlistRepository = playlistRepository;
            _mapper = mapper;
        }

        //[HttpGet("{id}")]
        //[ProducesResponseType(200, Type = typeof(Playlist))]
        //[ProducesResponseType(400)]
        //public IActionResult GetPlaylist(int id)
        //{
        //    if (!_playlistRepository.PlaylistExists(id))
        //        return NotFound();

        //    var playlist = _mapper.Map<PlaylistDto>(_playlistRepository.GetPlaylist(id));

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(playlist);
        //}
    }
}
