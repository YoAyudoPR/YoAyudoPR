using Microsoft.AspNetCore.Mvc;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Helpers;
using YoAyudoPR.Web.Infrastructure.Services;
using YoAyudoPR.Web.Models;

namespace YoAyudoPR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController>? _logger;
        private readonly IMemberService _memberService;
        private readonly IRoleService _roleService;
        private IUserService _userService;

        public MemberController(
            ILogger<MemberController>? logger,
            IMemberService memberService,
            IRoleService roleService,
            IUserService userService)
        {
            _logger = logger;
            _memberService = memberService;
            _roleService = roleService;
            _userService = userService;
        }

        [HttpGet("getusermemberships")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<MemberResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserMemberships([FromQuery] Guid? userGuid, CancellationToken cancellationToken)
        {
            if (userGuid == null)
            {
                return BadRequest(new ErrorResponseModel
                {
                    ErrorCode = "Bad Request",
                    ErrorMessage = "Must include the user guid parameter."
                });
            }

            var memberships = await _memberService.FindAll(x => x.User.Guid == userGuid, cancellationToken);

            return Ok(memberships);
        }

        [HttpGet("getall")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<MemberResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var memberships = await _memberService.FindAll(x => true, cancellationToken);

            return Ok(memberships);
        }

        [HttpGet("getorganizationmembers")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<MemberResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrganizationMembers([FromQuery] Guid? organizationGuid, CancellationToken cancellationToken)
        {
            if (organizationGuid == null)
            {
                return BadRequest(new ErrorResponseModel
                {
                    ErrorCode = "Bad Request",
                    ErrorMessage = "The organization guid is required."
                });
            }

            var memberships = await _memberService.FindAll(x => x.Organization.Guid == organizationGuid, cancellationToken);

            return Ok(memberships);
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MemberCreateRequest model, CancellationToken cancellationToken)
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

            var member = await _memberService.FindAll(x =>
            x.Organization.Guid == model.OrganizationGuid && x.User.Guid == model.UserGuid, cancellationToken);

            if (member.Any())
            {
                return BadRequest(new ErrorResponseModel
                {
                    ErrorCode = "Bad Request",
                    ErrorMessage = "This user is already part of this organization"
                });
            }

            await _memberService.Create(model, cancellationToken);

            return Ok(new SuccessResponseModel
            {
                SuccessMessage = "Membership created successfully."
            });
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] MemberUpdateRequest model, CancellationToken cancellationToken)
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

            await _memberService.Update(model, cancellationToken);

            return Ok(new SuccessResponseModel
            {
                SuccessMessage = "Membership was successfully."
            });
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
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
                        ErrorMessage = "The membership guid is required."
                    });
                }

                var dbMember = await _memberService.FirstByConditionAsync(x => x.Guid == guid, cancellationToken);

                if (dbMember == null)
                {
                    return NotFound(new ErrorResponseModel
                    {
                        ErrorCode = "Not Found",
                        ErrorMessage = "The membership details are not found."
                    });
                }

                await _memberService.Delete(guid.Value, cancellationToken);

                return Ok(new SuccessResponseModel
                {
                    SuccessMessage = "Membership was deleted."
                });

            }
            catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"deleting membership: {guid}");

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    ErrorCode = "Internal Server Error",
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet("getroles")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<RoleListResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoles(CancellationToken cancellationToken)
        {
            try 
            {
                var roles = await _roleService.FindAll(cancellationToken);

                return Ok(roles);
            }
            catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"fetching roles");

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    ErrorCode = "Internal Server Error",
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
