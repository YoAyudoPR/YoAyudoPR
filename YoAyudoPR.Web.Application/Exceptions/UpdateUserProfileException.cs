using System;
namespace YoAyudoPR.Web.Application.Exceptions
{
	public class UpdateUserProfileException : Exception
	{		

        public UpdateUserProfileException(string username, string errorCode)
        : base($"Update user profile failed. Username: {username}, Error code: {errorCode}")
        {
            this.Username = username;
            this.ErrorCode = errorCode;
        }

        public UpdateUserProfileException(string username, string errorCode, Exception innerException)
            : base($"Update user profile failed. Username: {username}, Error code: {errorCode}", innerException)
        {
            this.Username = username;
            this.ErrorCode = errorCode;
        }

        public string Username { get; private set; }
        public string ErrorCode { get; private set; }
    }
}

