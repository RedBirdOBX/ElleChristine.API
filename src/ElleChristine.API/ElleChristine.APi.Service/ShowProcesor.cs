using AutoMapper;
using ElleChristine.API.Data.DbContexts;
using ElleChristine.API.Data.Repositories;
using ElleChristine.API.Dtos;
using Microsoft.Extensions.Logging;

namespace ElleChristine.APi.Service
{
    public class ShowProcessor : IShowProcessor
    {
        private IElleChristineDbRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ShowProcessor> _logger;

        public ShowProcessor(IElleChristineDbRepository repository, IMapper mapper, ILogger<ShowProcessor> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(ElleChristineDbContext));
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ShowDto>> GetShowsAsync(bool showAll = false)
        {
            try
            {
                var shows = await _repository.GetShowsAsync(showAll);
                var results = _mapper.Map<IEnumerable<ShowDto>>(shows);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetShowsAsync)}", ex);
                throw ex;
            }
        }

        public async Task<ShowDto?> GetShowAsync(int showId)
        {
            try
            {
                var show = await _repository.GetShowAsync(showId);

                var results = _mapper.Map<ShowDto>(show);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetShowAsync)}", ex);
                throw;
            }
        }

        public async Task<ShowDto?> GetNextShowAsync()
        {
            try
            {
                var show = await _repository.GetNextShowAsync();
                var results = _mapper.Map<ShowDto>(show);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetNextShowAsync)}", ex);
                throw;
            }
        }

        public async Task<bool> DoesShowExistAsync(int showId)
        {
            return await _repository.DoesShowExistAsync(showId);
        }
    }
}