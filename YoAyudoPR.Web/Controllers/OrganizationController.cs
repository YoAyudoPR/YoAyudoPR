using Microsoft.AspNetCore.Mvc;
using System;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Infrastructure.Services;
using YoAyudoPR.Web.Models;

namespace YoAyudoPR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly ILogger<UserController>? _logger;
        private readonly IOrganizationService _organizationService;
        private readonly IMemberService _memberService;
        private readonly IEventService _eventService;

        public OrganizationController(
            ILogger<UserController> logger,
            IOrganizationService organizationService,
            IMemberService memberService,
            IEventService eventService)
        {
            _logger = logger;
            _organizationService = organizationService;
            _memberService = memberService;
            _eventService = eventService;
        }

        [HttpGet("getall")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<OrganizationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var organizations = await _organizationService.FindAll(x => true, cancellationToken);

            return Ok(organizations);
        }

        [HttpGet("getevents")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<EventListResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEvents([FromQuery] Guid? organizationGuid, CancellationToken cancellationToken)
        {
            if (organizationGuid == null)
            {
                return BadRequest(new ErrorResponseModel
                { 
                    ErrorCode = "Bad Request",
                    ErrorMessage = "Must include the organization guid parameter."
                });
            }

            var events = await _eventService.FindAll(x => x.Organization.Guid == organizationGuid, cancellationToken);

            return Ok(events);
        }

        [HttpGet("get")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OrganizationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] Guid? organizationGuid, CancellationToken cancellationToken)
        {
            if (organizationGuid == null)
            {
                return BadRequest(new ErrorResponseModel
                { 
                    ErrorCode = "Bad Request",
                    ErrorMessage = "Must include the organization guid parameter."
                });
            }

            var organization = await _organizationService.FirstByConditionAsync(x => x.Guid == organizationGuid, cancellationToken);

            return Ok(organization);
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] OrganizationCreateRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var organizacionGuid = await _organizationService.Create(model, cancellationToken);

            var memberModel = new MemberCreateRequest
            {
                OrganizationGuid = organizacionGuid,
                UserGuid = model.UserGuid,
                RoleId = 1 //Super Admin Role
            };

            await _memberService.Create(memberModel, cancellationToken);

            return Ok(new SuccessResponseModel
            {
                SuccessMessage = "Organization was created successfully."
            });
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] OrganizationUpdateRequest model, CancellationToken cancellationToken)
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

            await _organizationService.Update(model, cancellationToken);

            return Ok(new SuccessResponseModel
            {
                SuccessMessage = "Organization was updated successfully."
            });
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromQuery] Guid? organizationGuid, CancellationToken cancellationToken)
        {
            try
            {
                if (organizationGuid == null)
                {
                    return BadRequest(new ErrorResponseModel
                    {
                        ErrorCode = "Bad Request",
                        ErrorMessage = "The organization guid is required."
                    });
                }

                var organization = await _organizationService.FirstByConditionAsync(x => x.Guid == organizationGuid, cancellationToken);

                if (organization == null)
                {
                    return NotFound(new ErrorResponseModel
                    {
                        ErrorCode = "Not Found",
                        ErrorMessage = "The organization details are not found."
                    });
                }

                await _organizationService.Delete(organizationGuid.GetValueOrDefault(), cancellationToken);

                return Ok(new SuccessResponseModel
                {
                    SuccessMessage = "Organization was deleted."
                });

            }
            catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"deleting organization: {organizationGuid}");

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    ErrorCode = "Internal Server Error",
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
