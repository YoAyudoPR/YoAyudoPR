using Microsoft.AspNetCore.Mvc;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Application.Services;

namespace YoAyudoPR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController>? _logger;
        private readonly IMemberService _memberService;
        private readonly IRoleService _roleService;

        public MemberController(
            ILogger<MemberController>? logger,
            IMemberService memberService,
            IRoleService roleService)
        {
            _logger = logger;
            _memberService = memberService;
            _roleService = roleService;
        }

        [HttpGet("getusermemberships")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserMemberships([FromQuery] Guid? userGuid, CancellationToken cancellationToken)
        {
            if (userGuid == null)
            {
                return BadRequest("Must include the user guid parameter.");
            }

            var memberships = await _memberService.FindAll(x => x.User.Guid == userGuid, cancellationToken);

            return Ok(memberships);
        }

        [HttpGet("getorganizationmembers")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrganizationMembers([FromQuery] Guid? organizationGuid, CancellationToken cancellationToken)
        {
            if (organizationGuid == null)
            {
                return BadRequest("Must include the organization guid parameter.");
            }

            var memberships = await _memberService.FindAll(x => x.Organization.Guid == organizationGuid, cancellationToken);

            return Ok(memberships);
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] MemberCreateRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _memberService.Create(model, cancellationToken);

            return Ok(model);
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] MemberUpdateRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _memberService.Update(model, cancellationToken);

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

                var dbMember = await _memberService.FirstByConditionAsync(x => x.Guid == guid, cancellationToken);

                if (dbMember == null)
                {
                    return NotFound();
                }

                await _memberService.Delete(guid.GetValueOrDefault(), cancellationToken);

                return Ok();

            }
            catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"deleting user: {guid}");

                return Problem();
            }
        }

        [HttpGet("getroles")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoles(CancellationToken cancellationToken)
        {
            var roles = await _roleService.FindAll(cancellationToken);

            return Ok(roles);
        }
    }
}
