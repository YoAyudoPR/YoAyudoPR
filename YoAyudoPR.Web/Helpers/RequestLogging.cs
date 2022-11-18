using System.Text.Json;

namespace YoAyudoPR.Web.Helpers
{
    public class RequestLogging
    {
        private readonly ILogger<object> _logger;

        public RequestLogging(ILogger<object> logger)
        {
            _logger = logger;
        }

        public async Task LogError(HttpContext httpContext, Exception exception, object model = null)
        {
            string headerStr = "";
            string bodyStr = "";

            if (httpContext?.Request?.Headers is not null)
            {
                try
                {
                    headerStr = JsonSerializer.Serialize(httpContext.Request.Headers);
                }
                catch (Exception) { }
            }

            if (model is not null)
            {
                try
                {
                    bodyStr = JsonSerializer.Serialize(model);
                }
                catch (Exception) { }

            }

            if (!string.IsNullOrEmpty(headerStr) || !string.IsNullOrEmpty(bodyStr))
            {
                _logger.LogError(exception, "Request Header: {RequestHeader} | Request Body: {RequestBody}", headerStr, bodyStr);
            }

        }
    }
}
