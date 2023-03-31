using AutoMapper;
using MusicAPI.Dto;
using MusicAPI.Models;

namespace MusicAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Album, AlbumDto>()
                .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist));

            CreateMap<Album, AlbumDetailsDto>()
                .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
                .ForMember(dest => dest.Songs, opt => opt.MapFrom(src => src.Songs));

            CreateMap<Song, SongDto>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists))
                .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Album));

            CreateMap<Artist, ArtistDetailsDto>()
                .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.Albums));

            CreateMap<Album, AlbumRefDto>();
            CreateMap<Artist, ArtistDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserProfileDto>();
        }
    }
}
