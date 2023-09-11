using System;
namespace YoAyudoPR.Web.Application.Exceptions
{
	public class AuthenticationFailedException : Exception
	{
        public AuthenticationFailedException(string username, string errorCode)
        : base($"Authentication failed for user: {username}.")
        {
            this.Username = username;
            this.ErrorCode = errorCode;
        }

        public AuthenticationFailedException(string username, string errorCode, Exception innerException)
            : base($"Authentication failed for user: {username}.", innerException)
        {
            this.Username = username;
            this.ErrorCode = errorCode;
        }

        public string Username { get; private set; }
        public string ErrorCode { get; private set; }

    }
}

