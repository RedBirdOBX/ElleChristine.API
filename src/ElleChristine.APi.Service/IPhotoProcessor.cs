using ElleChristine.API.Dtos;

namespace ElleChristine.APi.Service
{
    public interface IPhotoProcessor
    {
        /// <summary>
        /// Gets all photos
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns>collection of PhotoDtos</returns>
        Task<IEnumerable<PhotoDto>> GetPhotosAsync(bool showAll);

        /// <summary>
        /// gets a single photo
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        Task<PhotoDto?> GetPhotoAsync(int photoId);

        /// <summary>
        /// checks to see if photo exists
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns>bool</returns>
        Task<bool> DoesPhotoExistAsync(int photoId);
    }
}
