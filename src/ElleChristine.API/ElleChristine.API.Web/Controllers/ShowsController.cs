using AutoMapper;
using ElleChristine.APi.Service;
using ElleChristine.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElleChristine.API.Web.Controllers
{
    /// <summary>
    /// ShowsController
    /// </summary>
    [Route("api/shows")]
    [ApiController]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ShowsController : ControllerBase
    {

        //private readonly ILogger<CategoriesController> _logger;
        private readonly IShowProcessor _processor;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        public ShowsController(IShowProcessor processor, IMapper mapper)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(IShowProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(Mapper));
        }

        /// <summary>
        /// lists all shows
        /// </summary>
        /// <returns>collection of shows</returns>
        /// <example>{baseUrl}/api/shows</example>
        /// <param name="showAll">flag to show both inactive and active</param>
        /// <response code="200">returns collection of shows</response>
        [HttpGet("", Name = "GetShows")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ShowDto>>> GetCategories(bool showAll = false)
        {
            try
            {
                //_logger.LogInformation("Getting categories");

                var showsDtos = await _processor.GetShowsAsync(showAll);
                //foreach (var categoryDto in categoriesDtos)
                //{
                //    categoryDto.Links.Add(UriLinkHelper.CreateLinkForCategoryWithinCollection(HttpContext.Request, categoryDto));
                //}
                return Ok(showsDtos);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error in {nameof(GetCategories)}", ex);
                return StatusCode(500, "An application error occurred.");
            }
        }
    }
}
