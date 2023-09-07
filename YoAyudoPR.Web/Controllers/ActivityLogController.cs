using Microsoft.AspNetCore.Mvc;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Application.Dtos;
using System;
using YoayudoPR.Web.Models.Response;

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
        [ProducesResponseType(typeof(List<ActivityLogResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var activityLogs = await _activityLogService.FindAll(x => true, cancellationToken);

            return Ok(activityLogs);
        }

        [HttpGet("getuserparticipations")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ActivityLogResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserParticipations([FromQuery] Guid? userGuid, CancellationToken cancellationToken)
        {
            if (userGuid == null)
            {
                return BadRequest(new ErrorResponseModel
                {
                    ErrorCode = "Bad Request",
                    ErrorMessage = "Must include the user guid parameter."
                });
            }

            var activityLogs = await _activityLogService.FindAll(x => x.User.Guid == userGuid, cancellationToken);

            return Ok(activityLogs);
        }

        [HttpGet("geteventparticipants")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ActivityLogResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventParticipants([FromQuery] Guid? eventGuid, CancellationToken cancellationToken)
        {
            if (eventGuid == null)
            {
                return BadRequest(new ErrorResponseModel
                {
                    ErrorCode = "Bad Request",
                    ErrorMessage = "Must include the event guid parameter."
                });
            }

            var activityLogs = await _activityLogService.FindAll(x => x.Event.Guid == eventGuid, cancellationToken);

            return Ok(activityLogs);
        }

        [HttpGet("get")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActivityLogResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] Guid? guid, CancellationToken cancellationToken)
        {
            if (guid == null)
            {
                return BadRequest(new ErrorResponseModel
                {
                    ErrorCode = "Bad Request",
                    ErrorMessage = "The activity log guid is required."
                });
            }

            var db_activityLog = await _activityLogService.FirstByConditionAsync(x => x.Guid == guid, cancellationToken);

            return Ok(db_activityLog);
        }

        [HttpPost("requestparticipation")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RequestParticipation([FromBody] ActivityLogCreateRequest model, CancellationToken cancellationToken)
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

            await _activityLogService.RequestPartiticpation(model, cancellationToken);

            return Ok(new SuccessResponseModel
            {
                SuccessMessage = "Participation request sent."
            });
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] ActivityLogUpdateRequest model, CancellationToken cancellationToken)
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

            await _activityLogService.Update(model, cancellationToken);

            return Ok(new SuccessResponseModel
            {
                SuccessMessage = "Activity log updated succesfully."
            });
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete([FromQuery] Guid? activityLogGuid, CancellationToken cancellationToken)
        {
            try
            {
                if (activityLogGuid == null)
                {
                    return BadRequest(new ErrorResponseModel
                    {
                        ErrorCode = "Bad Request",
                        ErrorMessage = "The activity log guid is required."
                    });
                }

                var dbActivityLog = await _activityLogService.FirstByConditionAsync(x => x.Guid == activityLogGuid, cancellationToken);

                if (dbActivityLog == null)
                {
                    return NotFound(new ErrorResponseModel
                    {
                        ErrorCode = "Not Found",
                        ErrorMessage = "The activity log details are not found."
                    });
                }

                await _activityLogService.Delete(activityLogGuid.Value, cancellationToken);

                return Ok(new SuccessResponseModel
                {
                    SuccessMessage = "Activity log deleted."
                });

            }
            catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"deleting activity log: {activityLogGuid}");

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    ErrorCode = "Internal Server Error",
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
