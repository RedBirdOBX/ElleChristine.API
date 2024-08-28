using ElleChristine.API.Dtos;

namespace ElleChristine.APi.Service
{
    public interface IVideoProcessor
    {
        /// <summary>
        /// Gets all videos
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns>collection of VideoDto</returns>
        Task<IEnumerable<VideoDto>> GetVideosAsync(bool showAll);

        /// <summary>
        /// gets a single video
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns>video</returns>
        Task<VideoDto?> GetVideoAsync(int videoId);

        /// <summary>
        /// checks to see if videoId is legit
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns>bool</returns>
        Task<bool> DoesVideoExistAsync(int videoId);
    }
}
