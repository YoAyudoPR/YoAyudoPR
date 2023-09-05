using Microsoft.AspNetCore.Mvc;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Application.Dtos;
using System;
using YoAyudoPR.Web.Models;

namespace YoAyudoPR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogController : ControllerBase
    {
        private readonly ILogger<ActivityLogController>? _logger;
        private readonly IActivityLogService _activityLogService;

        public ActivityLogController(
            ILogger<ActivityLogController>? logger,
            IActivityLogService activityLogService)
        {
            _logger = logger;
            _activityLogService = activityLogService;
        }

        [HttpGet("getall")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var activityLogs = await _activityLogService.FindAll(x => true, cancellationToken);

            return Ok(activityLogs);
        }

        [HttpGet("getuserparticipations")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserParticipations([FromQuery] Guid? userGuid, CancellationToken cancellationToken)
        {
            if (userGuid == null)
            {
                return BadRequest("Must include the user guid parameter.");
            }

            var activityLogs = await _activityLogService.FindAll(x => x.User.Guid == userGuid, cancellationToken);

            return Ok(activityLogs);
        }

        [HttpGet("geteventparticipants")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventParticipants([FromQuery] Guid? eventGuid, CancellationToken cancellationToken)
        {
            if (eventGuid == null)
            {
                return BadRequest("Must include the event guid parameter.");
            }

            var activityLogs = await _activityLogService.FindAll(x => x.Event.Guid == eventGuid, cancellationToken);

            return Ok(activityLogs);
        }

        [HttpGet("get")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] Guid? guid, CancellationToken cancellationToken)
        {
            if (guid == null)
            {
                return BadRequest("Must include the activity log guid parameter.");
            }

            var db_activityLog = await _activityLogService.FirstByConditionAsync(x => x.Guid == guid, cancellationToken);

            return Ok(db_activityLog);
        }

        [HttpPost("requestparticipation")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RequestParticipation([FromBody] ActivityLogCreateRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _activityLogService.RequestPartiticpation(model, cancellationToken);

            return Ok(model);
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] ActivityLogUpdateRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _activityLogService.Update(model, cancellationToken);

            return Ok();
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete([FromQuery] Guid? guid, CancellationToken cancellationToken)
        {
            try
            {
                if (guid == null)
                {
                    return BadRequest();
                }

                var dbActivityLog = await _activityLogService.FirstByConditionAsync(x => x.Guid == guid, cancellationToken);

                if (dbActivityLog == null)
                {
                    return NotFound();
                }

                await _activityLogService.Delete(guid.GetValueOrDefault(), cancellationToken);

                return Ok();

            }
            catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"deleting activity log: {guid}");

                return Problem();
            }
        }
    }
}
