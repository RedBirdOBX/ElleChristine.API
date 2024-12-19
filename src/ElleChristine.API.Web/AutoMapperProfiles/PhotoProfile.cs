using AutoMapper;
using ElleChristine.API.Data.Entities;
using ElleChristine.API.Dtos;

namespace ElleChristine.API.Web.AutoMapperProfiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            // source, destination
            CreateMap<Photo?, PhotoDto>();
        }
    }
}
