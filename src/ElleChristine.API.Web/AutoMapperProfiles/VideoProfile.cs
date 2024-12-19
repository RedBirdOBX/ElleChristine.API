using AutoMapper;
using ElleChristine.API.Data.Entities;
using ElleChristine.API.Dtos;

namespace ElleChristine.API.Web.AutoMapperProfiles
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            // source, destination
            CreateMap<Video?, VideoDto>();
        }
    }
}
