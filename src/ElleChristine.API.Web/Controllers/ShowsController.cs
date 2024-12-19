using AutoMapper;
using ElleChristine.APi.Service;
using ElleChristine.API.Dtos;
using ElleChristine.API.Dtos.Filters;
using ElleChristine.API.Web.Controllers.ResponseHelpers;
using Microsoft.AspNetCore.Mvc;

namespace ElleChristine.API.Web.Controllers
{
    /// <summary>
    /// ShowsController
    /// </summary>
    [Route("api/shows")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ShowsController : ControllerBase
    {

        private readonly ILogger<ShowsController> _logger;
        private readonly IShowProcessor _processor;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        public ShowsController(IShowProcessor processor, IMapper mapper, ILogger<ShowsController> logger)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(IShowProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(Mapper));
            _logger = logger;
        }

        /// <summary>
        /// returns collection of all shows
        /// </summary>
        /// <returns>collection of shows</returns>
        /// <example>{baseUrl}/api/shows</example>
        /// <response code="200">returns collection of shows</response>
        [HttpGet("", Name = "GetShows")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ShowDto>>> GetShows()
        {
            try
            {
                _logger.LogInformation("Getting Shows data.");

                var showsDtos = await _processor.GetShowsAsync();
                foreach (var showDto in showsDtos)
                {
                    showDto.Links.Add(UriLinkHelper.CreateLinkForShowWithinCollection(HttpContext.Request, showDto));
                }
                return Ok(showsDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetShows)}: {ex}");
                return StatusCode(500, "An application error occurred.");
            }
        }

        /// <summary>
        /// returns collection of filtered shows
        /// </summary>
        /// <returns>collection of shows</returns>
        /// <example>{baseUrl}/api/shows</example>
        /// <param name="filter">filter for shows</param>
        /// <response code="200">returns collection of filtered shows</response>
        [HttpPost("filter", Name = "GetShowsFiltered")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ShowDto>>> GetShowsFiltered(ShowFilter filter)
        {
            try
            {
                _logger.LogInformation("Getting Filtered Shows data.");

                var showsDtos = await _processor.GetShowsFilteredAsync(filter);
                foreach (var showDto in showsDtos)
                {
                    showDto.Links.Add(UriLinkHelper.CreateLinkForShowWithinCollection(HttpContext.Request, showDto));
                }
                return Ok(showsDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetShowsFiltered)}: {ex}");
                return StatusCode(500, "An application error occurred.");
            }
        }

        /// <summary>
        /// returns single show
        /// </summary>
        /// <param name="showId"></param>
        /// <returns>ShowDto</returns>
        /// <example>{baseUrl}/api/shows/{showId}</example>
        /// <response code="200">returns requested show</response>
        [HttpGet("{showId}", Name = "GetShow")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShowDto>> GetShow(int showId)
        {
            try
            {
                if (!await _processor.DoesShowExistAsync(showId))
                {
                    return NotFound($"show {showId} not found.");
                }

                var showDto = await _processor.GetShowAsync(showId) ?? new ShowDto();
                showDto = UriLinkHelper.CreateLinksForShow(HttpContext.Request, showDto);
                return Ok(showDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetShow)}: {ex}");
                return StatusCode(500, $"An application error occurred. {ex}");
            }
        }

        /// <summary>
        /// returns next upcoming show
        /// </summary>
        /// <returns>ShowDto</returns>
        /// <example>{baseUrl}/api/shows/nextshow</example>
        /// <response code="200">returns next upcoming show</response>
        [HttpGet("nextshow", Name = "NextShow")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShowDto>> NextShow()
        {
            try
            {
                var showDto = await _processor.GetNextShowAsync() ?? new ShowDto();
                showDto = UriLinkHelper.CreateLinksForShow(HttpContext.Request, showDto);
                return Ok(showDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(NextShow)}: {ex}");
                return StatusCode(500, "An application error occurred.");
            }
        }
    }
}
