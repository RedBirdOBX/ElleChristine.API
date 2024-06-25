using AutoMapper;
using ElleChristine.API.Data.DbContexts;
using ElleChristine.API.Data.Repositories;
using ElleChristine.API.Dtos;
using Microsoft.Extensions.Logging;

namespace ElleChristine.APi.Service
{
    public class PhotoProcessor : IPhotoProcessor
    {
        private IElleChristineDbRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PhotoProcessor> _logger;

        public PhotoProcessor(IElleChristineDbRepository repository, IMapper mapper, ILogger<PhotoProcessor> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(ElleChristineDbContext));
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets all photos
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns>collection of PhotoDtos</returns>
        public async Task<IEnumerable<PhotoDto>> GetPhotosAsync(bool showAll = false)
        {
            try
            {
                var photos = await _repository.GetPhotosAsync(showAll);
                var results = _mapper.Map<IEnumerable<PhotoDto>>(photos);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetPhotosAsync)}", ex);
                throw ex;
            }
        }

        /// <summary>
        /// gets single show
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns>PhotoDto</returns>
        public async Task<PhotoDto?> GetPhotoAsync(int photoId)
        {
            try
            {
                var photo = await _repository.GetPhotoAsync(photoId);
                var results = _mapper.Map<PhotoDto>(photo);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetPhotoAsync)}", ex);
                throw;
            }
        }

        /// <summary>
        /// checks to see if photoId is legit
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns>bool</returns>
        public async Task<bool> DoesPhotoExistAsync(int photoId)
        {
            return await _repository.DoesPhotoExistAsync(photoId);
        }
    }
}