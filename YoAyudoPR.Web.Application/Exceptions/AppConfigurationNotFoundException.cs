using System;
namespace YoAyudoPR.Web.Application.Exceptions
{
	public class AppConfigurationNotFoundException : Exception
	{
        public AppConfigurationNotFoundException(string? appKey)
        : base($"App Configuration not found | App Key: {appKey}")
        {
            AppKey = appKey;
        }

        public AppConfigurationNotFoundException(string? paramType, Exception innerException)
            : base($"App Configuration not found | App Key: {paramType}", innerException)
        {
            AppKey = paramType;
        }

        public string? AppKey { get; set; }
    }
}

