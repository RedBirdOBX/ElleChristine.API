using ElleChristine.API.Dtos;

namespace ElleChristine.APi.Service
{
    public interface IShowProcessor
    {
        /// <summary>
        /// Gets all shows
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns>collection of ShowDtos</returns>
        Task<IEnumerable<ShowDto>> GetShowsAsync(bool showAll);

        /// <summary>
        /// gets a single show
        /// </summary>
        /// <param name="showId"></param>
        /// <returns></returns>
        Task<ShowDto?> GetShowAsync(int showId);

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
