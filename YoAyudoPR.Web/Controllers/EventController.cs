using Microsoft.AspNetCore.Mvc;
using System;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Helpers;
using YoAyudoPR.Web.Models;
using YoAyudoPR.Web.Pages;

namespace YoAyudoPR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController>? _logger;
        private readonly IEventService _eventService;
        private readonly ICategoryService _categoryService;
        public EventController(
            ILogger<EventController> logger,
            IEventService eventService,
            ICategoryService categoryService)
        {
            _logger = logger;
            _eventService = eventService;
            _categoryService = categoryService;
        }

        [HttpGet("searchevents")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<EventListResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchEvents(CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    return BadRequest(new ErrorResponseModel
                    {
                        ErrorCode = "Bad Request",
                        ErrorMessage = message
                    });
                }

                var events = await _eventService.FindAll(x => true, cancellationToken);

                return Ok(events);
            } catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"searching events");

                return StatusCode(StatusCodes.Status500InternalServerError ,new ErrorResponseModel
                {
                    ErrorCode = "Internal Server Error",
                    ErrorMessage = ex.Message,
                });
            }
        }

        [HttpGet("get")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] Guid? guid, CancellationToken cancellationToken)
        {
            if (guid == null)
            {
                return BadRequest(new ErrorResponseModel
                {
                    ErrorCode = "Bad Request",
                    ErrorMessage = "The event guid is required."
                });
            }

            var db_event = await _eventService.FirstByConditionAsync(x 
                => x.Isdeleted == false && x.Isactive == true && x.Guid == guid, cancellationToken);

            if (db_event == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    ErrorCode = "Not Found",
                    ErrorMessage = "The event details are not found."
                });
            }

            return Ok(db_event);
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] EventCreateRequest model, CancellationToken cancellationToken)
        {
            try
            { 
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    return BadRequest(new ErrorResponseModel
                    {
                        ErrorCode = "Bad Request",
                        ErrorMessage = message
                    });
                }

                await _eventService.Create(model, cancellationToken);

                return Ok(new SuccessResponseModel
                {
                    SuccessMessage = "Event was created successfully."
                });
            }
            catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"create event");

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    ErrorCode = "Internal Server Error",
                    ErrorMessage = ex.Message,
                });
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] EventUpdateRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return BadRequest(new ErrorResponseModel
                {
                    ErrorCode = "Bad Request",
                    ErrorMessage = message
                });
            }

            await _eventService.Update(model, cancellationToken);

            return Ok(new SuccessResponseModel
            {
                SuccessMessage = "Event was updated successfully."
            });
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromQuery] Guid? guid, CancellationToken cancellationToken)
        {
            try
            {
                if (guid == null)
                {
                    return BadRequest(new ErrorResponseModel
                    {
                        ErrorCode = "Bad Request",
                        ErrorMessage = "The event guid is required."
                    });
                }

                var dbEvent = await _eventService.FirstByConditionAsync(x => x.Guid == guid, cancellationToken);

                if (dbEvent == null)
                {
                    return NotFound(new ErrorResponseModel {
                        ErrorCode = "Not Found",
                        ErrorMessage = "The event details are not found."
                    });
                }

                await _eventService.Delete(guid.Value, cancellationToken);

                return Ok(new SuccessResponseModel
                {
                    SuccessMessage = "Event was deleted."
                });

            }
            catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"deleting event: {guid}");

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    ErrorCode = "Internal Server Error",
                    ErrorMessage = ex.Message,
                });
            }
        }

        [HttpGet("getcategories")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<CategoryListResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            try
            { 
                var categories = await _categoryService.FindAll(cancellationToken);
            
                return Ok(categories);
            }
            catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"fetching categories");

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    ErrorCode = "Internal Server Error",
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
