using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Yoayudopr.Web.Models.Authentication;
using YoayudoPR.Web.Models.Response;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Application.Dtos.Authentication;
using YoAyudoPR.Web.Application.Exceptions;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Extensions;

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
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel model, CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation($"Starting to authenticate user: {model.Email}");

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

            var user = await _userService.Authenticate(model.Email!, model.Password!, cancellationToken);

            if (user == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    ErrorCode = "Not Found",
                    ErrorMessage = "This user was not found."
                });
            }
            else
            {
                if (user.Isactive == false)
                {
                    return NotFound(new ErrorResponseModel
                    {
                        ErrorCode = "Not Found",
                        ErrorMessage = "This user was deactivated."
                    });
                } 
                else if (user.Isdeleted == true)
                {
                    return NotFound(new ErrorResponseModel
                    {
                        ErrorCode = "Not Found",
                        ErrorMessage = "This user was deleted."
                    });
                }
            }

            //string secret = _configuration.GetValue<string>("JwtSecret");

            //var token = await _userService.GenerateJWT(user, secret, cancellationToken);

            //return Ok(new { token = token });

            return Ok(new { userGuid = user.Guid });
        }

        [HttpGet("searchusers")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchUsers(CancellationToken cancellationToken)
        {
            var users = await _userService.FindAll(user => user.Isdeleted == false, cancellationToken);

            return Ok(users);
        }

        [HttpGet("get")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] Guid? userGuid, CancellationToken cancellationToken)
        {
            if (userGuid == null)
            {
                return BadRequest(new ErrorResponseModel
                {
                    ErrorCode = "Bad Request",
                    ErrorMessage = "The user guid is required."
                });
            }

            var user = await _userService.FirstByConditionAsync(user => user.Guid == userGuid, cancellationToken);
            
            return Ok(user);
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest model, CancellationToken cancellationToken)
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

            if (!string.IsNullOrEmpty(model.Phone))
            {
                model.Phone = model.Phone?.Length > 10 ?
                    $"{model.Phone[..1]} ({model.Phone[1..4]}) {model.Phone[4..7]}-{model.Phone[7..11]}" :
                    $"({model.Phone?[..3]}) {model.Phone?[3..6]}-{model.Phone?[6..10]}";
            }

            await _userService.Create(model, cancellationToken);

            return Ok(new SuccessResponseModel
            {
                SuccessMessage = "User created successfully."
            });
        }
        
        [HttpPut("update")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest model, CancellationToken cancellationToken)
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

            await _userService.Update(model, cancellationToken);

            return Ok(new SuccessResponseModel
            {
                SuccessMessage = "User updated successfully."
            });
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromQuery] Guid? userGuid, CancellationToken cancellationToken)
        {
            try
            {
                if (userGuid == null)
                {
                    return BadRequest(new ErrorResponseModel
                    {
                        ErrorCode = "Bad Request",
                        ErrorMessage = "The user guid is required."
                    });
                }

                var user = await _userService.FirstByConditionAsync(x => x.Guid == userGuid, cancellationToken);

                if (user == null)
                {
                    return NotFound(new ErrorResponseModel
                    {
                        ErrorCode = "Not Found",
                        ErrorMessage = "The user details are not found."
                    });
                }

                await _userService.Delete(userGuid.GetValueOrDefault(), cancellationToken);

                return Ok(new SuccessResponseModel
                {
                    SuccessMessage = "User was deleted."
                });

            } catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"deleting user: {userGuid}");

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    ErrorCode = "Internal Server Error",
                    ErrorMessage = ex.Message,
                });
            }
        }

        [HttpPost("forgotpassword")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model, CancellationToken cancellationToken)
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

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                return BadRequest(new ErrorResponseModel
                {
                    ErrorCode = "Bad Request",
                    ErrorMessage = "Please provide email or username."
                });
            }

            var userUpdated = await _userService.ForgotPassword(model, cancellationToken);

            return userUpdated ? Ok(new SuccessResponseModel
            {
                SuccessMessage = "An email has been sent to reset your password."
            }) : NotFound(new ErrorResponseModel
            {
                ErrorCode = "Not Found",
                ErrorMessage = "The user details are not found."
            });
            
        }

        [HttpPut("changepassword")]
        [ProducesResponseType(typeof(SuccessResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest model, CancellationToken cancellationToken)
        {
            _ = Guid.TryParse(HttpContext.Items["UserGuid"]?.ToString() ?? "", out Guid userGuid);

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

                await _userService.ChangePassword(model, userGuid, cancellationToken);

                return Ok(new SuccessResponseModel
                {
                    SuccessMessage = "Password changed successfully."
                });

            } catch (UserNotFoundException ex)
            {
                return NotFound(new ErrorResponseModel
                {
                    ErrorCode = "Not Found",
                    ErrorMessage = ex.ErrorMessage
                });
            } catch (ChangePasswordFailedException ex)
            {
                return BadRequest(new ErrorResponseModel
                {
                    ErrorCode = "Bad Request",
                    ErrorMessage = ex.ErrorMessage
                });
            }
            catch (Exception ex)
            {
                var requestLogging = new Helpers.RequestLogging(_logger);
                await requestLogging.LogError(HttpContext, ex, $"changing password: {userGuid}");

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    ErrorCode = "Internal Server Error",
                    ErrorMessage = ex.Message
                });
            }
            
        }
    }
}
