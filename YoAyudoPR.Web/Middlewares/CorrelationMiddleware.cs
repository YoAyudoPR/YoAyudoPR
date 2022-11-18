using Microsoft.Extensions.Primitives;

namespace YoAyudoPR.Web.Middlewares
{
    public class CorrelationMiddleware
    {
        private const string CorrelationIdKey = "X-Correlation-Id";
        private readonly RequestDelegate _next;

        public CorrelationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool couldGetValue = context.Request.Headers
                .TryGetValue(CorrelationIdKey, out StringValues correlationIdString);

            if (!couldGetValue || string.IsNullOrWhiteSpace(correlationIdString)
                || !Guid.TryParse(correlationIdString, out Guid correlationId))
            {
                correlationId = Guid.NewGuid();
                context.Request.Headers[CorrelationIdKey] = correlationId.ToString();
            }
            context.Response.Headers[CorrelationIdKey] = correlationId.ToString();

            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
    }

    public static class CorrelationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationId(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationMiddleware>();
        }
    }
    
}
