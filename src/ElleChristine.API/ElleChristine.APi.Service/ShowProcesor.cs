using AutoMapper;
using ElleChristine.API.Data.DbContexts;
using ElleChristine.API.Data.Entities;
using ElleChristine.API.Data.Repositories;
using ElleChristine.API.Dtos;

namespace ElleChristine.APi.Service
{
    public class ShowProcessor : IShowProcessor
    {
        private IElleChristineDbRepository _repository;
        private readonly IMapper _mapper;
        //private readonly ILogger<CategoryProcessor> _logger;

        public ShowProcessor(IElleChristineDbRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(ElleChristineDbContext));
            _mapper = mapper;
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
                //_logger.LogError($"Error in {nameof(GetCategoriesAsync)}", ex);
                throw ex;
            }
        }

        //public async Task<Show?> GetShowAsync(int showId)
        //{
        //    try
        //    {
        //        var show = await _repository.GetShowAsync(categoryId, includeSubCategories);

        //        var results = _mapper.Map<CategoryDto>(category);
        //        return results;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error in {nameof(GetCategoryAsync)}", ex);
        //        throw;
        //    }
        //}
    }
}