using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace YoAyudoPR.Web.Middlewares
{
    public class JwtMiddleware
    {
        ILogger<JwtMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(ILogger<JwtMiddleware> logger, RequestDelegate next, IConfiguration configuration)
        {
            _logger = logger;
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                string secret = _configuration.GetValue<string>("JwtSecret");

                var key = Encoding.ASCII.GetBytes(secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userGuid = jwtToken.Claims.First(x => x.Type == "userguid").Value;
                context.Items["UserGuid"] = userGuid;

                var role = jwtToken.Claims.First(x => x.Type == "role").Value;
                context.Items["Role"] = role;

                var rolePermissions = jwtToken.Claims.First(x => x.Type == "rolePermissions")?.Value;
                context.Items["RolePermissions"] = rolePermissions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking JWT validity");
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }


    }
}
