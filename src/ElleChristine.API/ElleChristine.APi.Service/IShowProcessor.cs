using ElleChristine.API.Dtos;

namespace ElleChristine.APi.Service
{
    public interface IShowProcessor
    {
        Task<IEnumerable<ShowDto>> GetShowsAsync(bool showAll);

        Task<ShowDto?> GetShowAsync(int showId);

        Task<bool> DoesShowExistAsync(int showId);
    }
}
