using AutoMapper;
using ElleChristine.APi.Service;
using ElleChristine.API.Dtos;
using ElleChristine.API.Web.Controllers.ResponseHelpers;
using Microsoft.AspNetCore.Mvc;

namespace ElleChristine.API.Web.Controllers
{
    /// <summary>
    /// VideosController
    /// </summary>
    [Route("api/videos")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class VideosController : ControllerBase
    {

        private readonly ILogger<VideosController> _logger;
        private readonly IVideoProcessor _processor;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        public VideosController(IVideoProcessor processor, IMapper mapper, ILogger<VideosController> logger)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(IVideoProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(Mapper));
            _logger = logger;
        }

        /// <summary>
        /// lists all videos
        /// </summary>
        /// <returns>collection of videos</returns>
        /// <example>{baseUrl}/api/videos</example>
        /// <param name="showAll">flag to show both inactive and active</param>
        /// <response code="200">returns collection of videos</response>
        [HttpGet("", Name = "GetVideos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VideoDto>>> GetVidoes(bool showAll = false)
        {
            try
            {
                var videosDtos = await _processor.GetVideosAsync(showAll);
                foreach (var videoDto in videosDtos)
                {
                    videoDto.Links.Add(UriLinkHelper.CreateLinkForVideoWithinCollection(HttpContext.Request, videoDto));
                }
                return Ok(videosDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetVidoes)}: {ex}");
                return StatusCode(500, "An application error occurred.");
            }
        }

        /// <summary>
        /// returns single video
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns>VideoDto</returns>
        /// <example>{baseUrl}/api/videos/{videoId}</example>
        /// <response code="200">returns requested video</response>
        [HttpGet("{videoId}", Name = "GetVideo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PhotoDto>> GetVideo(int videoId)
        {
            try
            {
                if (!await _processor.DoesVideoExistAsync(videoId))
                {
                    return NotFound($"video {videoId} not found.");
                }

                var videoDto = await _processor.GetVideoAsync(videoId) ?? new VideoDto();
                videoDto = UriLinkHelper.CreateLinksForVideo(HttpContext.Request, videoDto);
                return Ok(videoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetVideo)}: {ex}");
                return StatusCode(500, $"An application error occurred. {ex}");
            }
        }

    }
}
