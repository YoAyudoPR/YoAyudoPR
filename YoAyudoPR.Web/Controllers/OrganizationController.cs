using Microsoft.AspNetCore.Mvc;
using System;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Infrastructure.Services;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var organizations = await _organizationService.FindAll(x => true, cancellationToken);

            return Ok(organizations);
        }

        [HttpGet("getevents")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEvents([FromQuery] Guid? guid, CancellationToken cancellationToken)
        {
            if (guid == null)
            {
                return BadRequest("Must include the organization guid parameter.");
            }

            var events = await _eventService.FindAll(x => x.Organization.Guid == guid, cancellationToken);

            return Ok(events);
        }

        [HttpGet("get")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] Guid? guid, CancellationToken cancellationToken)
        {
            if (guid == null)
            {
                return BadRequest("Must include the organization guid parameter.");
            }

            var organization = await _organizationService.FirstByConditionAsync(x => x.Guid == guid, cancellationToken);

            return Ok(organization);
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

            return Ok(model);
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] OrganizationUpdateRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _organizationService.Update(model, cancellationToken);

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

                var organization = await _organizationService.FirstByConditionAsync(x => x.Guid == guid, cancellationToken);

                if (organization == null)
                {
                    return NotFound();
                }

                await _organizationService.Delete(guid.GetValueOrDefault(), cancellationToken);

                return Ok();

            }
            catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"deleting organization: {guid}");

                return Problem();
            }
        }
    }
}
