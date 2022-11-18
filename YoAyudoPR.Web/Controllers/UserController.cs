using Microsoft.AspNetCore.Mvc;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YoAyudoPR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController>? _logger;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService,
            IConfiguration configuration)
        {
            _userService = userService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("auth")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel model, CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation($"Starting to authenticate user: {model.Email}");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userService.Authenticate(model.Email!, model.Password!, cancellationToken);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                if (user.Isactive == false)
                {
                    return NotFound();
                } 
                else if (user.Isdeleted == false)
                {
                    return NotFound();
                }
            }

            string secret = _configuration.GetValue<string>("JwtSecret");

            var token = await _userService.GenerateJWT(user, secret, cancellationToken);

            _logger?.LogInformation($"Starting to authenticate user: {model.Email}");

            return Ok(new { token = token });
        }

        [HttpGet("searchusers")]
        [Produces("application/json")]
        public async Task<IActionResult> SearchUsers(CancellationToken cancellationToken)
        {
            var users = await _userService.FindAll(user => user.Isdeleted == false, cancellationToken);

            return Ok(users);
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

            var user = await _userService.FirstByConditionAsync(user => user.Guid == guid, cancellationToken);
            
            return Ok(user);
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _userService.Create(model, cancellationToken);

            return Ok(model);
        }
        
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _userService.Update(model, cancellationToken);

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

                var user = await _userService.FirstByConditionAsync(x => x.Guid == guid, cancellationToken);

                if (user == null)
                {
                    return NotFound();
                }

                await _userService.Delete(guid.GetValueOrDefault(), cancellationToken);

                return Ok();

            } catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"deleting user: {guid}");

                return Problem();
            }
        }
    }
}
