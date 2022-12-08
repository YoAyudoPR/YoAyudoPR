using Microsoft.AspNetCore.Mvc;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Models;

namespace YoAyudoPR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly ILogger<EventController>? _logger;
        private readonly IEventService _eventService;
        public EventController(
            ILogger<EventController> logger,
            IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }

        [HttpGet("searchevents")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchEvents(CancellationToken cancellationToken)
        {
            var events = await _eventService.FindAll(x => true, cancellationToken);

            return View(events);
        }

        [HttpGet("get")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] Guid? guid, CancellationToken cancellationToken)
        {
            if (guid == null)
            {
                return BadRequest();
            }

            var db_event = await _eventService.FirstByConditionAsync(x => true, cancellationToken);

            return Ok(db_event);
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] EventCreateRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _eventService.Create(model, cancellationToken);

            return Ok(model);
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] EventUpdateRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _eventService.Update(model, cancellationToken);

            return Ok();
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromQuery] Guid? guid, CancellationToken cancellationToken)
        {
            try
            {
                if (guid == null)
                {
                    return BadRequest();
                }

                var user = await _eventService.FirstByConditionAsync(x => x.Guid == guid, cancellationToken);

                if (user == null)
                {
                    return NotFound();
                }

                await _eventService.Delete(guid.GetValueOrDefault(), cancellationToken);

                return Ok();

            }
            catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"deleting user: {guid}");

                return Problem();
            }
        }
    }
}
