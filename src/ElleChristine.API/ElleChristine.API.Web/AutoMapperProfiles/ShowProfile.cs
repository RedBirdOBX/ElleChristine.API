using AutoMapper;
using ElleChristine.API.Data.Entities;
using ElleChristine.API.Dtos;

namespace ElleChristine.API.Web.AutoMapperProfiles
{
    public class ShowProfile : Profile
    {
        public ShowProfile()
        {
            // source, destination
            CreateMap<Show?, ShowDto>();
        }
    }
}
