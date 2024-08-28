using ElleChristine.API.Data.Entities;

namespace ElleChristine.API.Data.Repositories
{
    public interface IElleChristineDbRepository
    {
        // shows
        Task<IEnumerable<Show>> GetShowsAsync(bool showAll);

        Task<Show?> GetShowAsync(int showId);

        Task<Show?> GetNextShowAsync();

        Task<bool> DoesShowExistAsync(int showId);

        // photos
        Task<IEnumerable<Photo>> GetPhotosAsync(bool showAll);

        Task<Photo?> GetPhotoAsync(int photoId);

        Task<bool> DoesPhotoExistAsync(int photoId);

        // videos
        Task<IEnumerable<Video>> GetVideosAsync(bool showAll);

        Task<Video?> GetVideoAsync(int videoId);

        Task<bool> DoesVideoExistAsync(int videoId);
    }
}
