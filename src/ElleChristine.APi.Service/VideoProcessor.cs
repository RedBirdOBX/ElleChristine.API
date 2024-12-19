using AutoMapper;
using ElleChristine.API.Data.DbContexts;
using ElleChristine.API.Data.Repositories;
using ElleChristine.API.Dtos;
using Microsoft.Extensions.Logging;

namespace ElleChristine.APi.Service
{
    public class VideoProcessor : IVideoProcessor
    {
        private IElleChristineDbRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<VideoProcessor> _logger;

        public VideoProcessor(IElleChristineDbRepository repository, IMapper mapper, ILogger<VideoProcessor> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(ElleChristineDbContext));
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets all videos
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns>collection of VideoDto</returns>
        public async Task<IEnumerable<VideoDto>> GetVideosAsync(bool showAll = false)
        {
            try
            {
                var videos = await _repository.GetVideosAsync(showAll);
                var results = _mapper.Map<IEnumerable<VideoDto>>(videos);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetVideosAsync)}", ex);
                throw ex;
            }
        }

        /// <summary>
        /// gets single video
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns>VideoDto</returns>
        public async Task<VideoDto?> GetVideoAsync(int videoId)
        {
            try
            {
                var video = await _repository.GetVideoAsync(videoId);
                var results = _mapper.Map<VideoDto>(video);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetVideoAsync)}", ex);
                throw;
            }
        }

        /// <summary>
        /// checks to see if videoId is legit
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns>bool</returns>
        public async Task<bool> DoesVideoExistAsync(int videoId)
        {
            return await _repository.DoesVideoExistAsync(videoId);
        }
    }
}