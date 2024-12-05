using ElleChristine.API.Dtos;
using ElleChristine.API.Dtos.Filters;

namespace ElleChristine.APi.Service
{
    public interface IShowProcessor
    {
        /// <summary>
        /// gets a single show
        /// </summary>
        /// <param name="showId"></param>
        /// <returns>ShowDto</returns>
        Task<ShowDto?> GetShowAsync(int showId);

        /// <summary>
        /// returns a list of shows
        /// </summary>
        /// <returns>collection of ShowDtos</returns>
        Task<IEnumerable<ShowDto>> GetShowsAsync();

        /// <summary>
        /// returns a list of filtered shows
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>collection of ShowDtos</returns>
        Task<IEnumerable<ShowDto>> GetShowsFilteredAsync(ShowFilter filter);

        /// <summary>
        /// Gets the next upcoming show
        /// </summary>
        /// <returns>ShowDto</returns>
        Task<ShowDto?> GetNextShowAsync();

        /// <summary>
        /// checks to see if showId is legit
        /// </summary>
        /// <param name="showId"></param>
        /// <returns>bool</returns>
        Task<bool> DoesShowExistAsync(int showId);
    }
}
