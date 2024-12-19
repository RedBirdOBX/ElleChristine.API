using AutoMapper;
using ElleChristine.APi.Service;
using ElleChristine.API.Dtos;
using ElleChristine.API.Web.Controllers.ResponseHelpers;
using Microsoft.AspNetCore.Mvc;

namespace ElleChristine.API.Web.Controllers
{
    /// <summary>
    /// PhotosController
    /// </summary>
    [Route("api/photos")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class PhotosController : ControllerBase
    {

        private readonly ILogger<PhotosController> _logger;
        private readonly IPhotoProcessor _processor;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        public PhotosController(IPhotoProcessor processor, IMapper mapper, ILogger<PhotosController> logger)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(IPhotoProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(Mapper));
            _logger = logger;
        }

        /// <summary>
        /// lists all photos
        /// </summary>
        /// <returns>collection of photos</returns>
        /// <example>{baseUrl}/api/photos</example>
        /// <param name="showAll">flag to show both inactive and active</param>
        /// <response code="200">returns collection of photos</response>
        [HttpGet("", Name = "GetPhotos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetPhotos(bool showAll = false)
        {
            try
            {
                var photoDtos = await _processor.GetPhotosAsync(showAll);
                foreach (var photoDto in photoDtos)
                {
                    photoDto.Links.Add(UriLinkHelper.CreateLinkForPhotoWithinCollection(HttpContext.Request, photoDto));
                }
                return Ok(photoDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetPhotos)}: {ex}");
                return StatusCode(500, "An application error occurred.");
            }
        }

        /// <summary>
        /// returns single photo
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns>PhotoDto</returns>
        /// <example>{baseUrl}/api/photos/{showId}</example>
        /// <response code="200">returns requested photo</response>
        [HttpGet("{photoId}", Name = "GetPhoto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PhotoDto>> GetPhoto(int photoId)
        {
            try
            {
                if (!await _processor.DoesPhotoExistAsync(photoId))
                {
                    return NotFound($"photo {photoId} not found.");
                }

                var photoDto = await _processor.GetPhotoAsync(photoId) ?? new PhotoDto();
                photoDto = UriLinkHelper.CreateLinksForPhoto(HttpContext.Request, photoDto);
                return Ok(photoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetPhoto)}: {ex}");
                return StatusCode(500, $"An application error occurred. {ex}");
            }
        }
    }
}
