using ElleChristine.API.Data.Entities;
using ElleChristine.API.Dtos;

namespace ElleChristine.APi.Service
{
    public interface IShowProcessor
    {
        Task<IEnumerable<ShowDto>> GetShowsAsync(bool showAll);
    }
}
