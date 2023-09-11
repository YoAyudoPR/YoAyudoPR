using System;
namespace YoAyudoPR.Web.Application.Exceptions
{
	public class ChangePasswordFailedException : Exception
	{
        public ChangePasswordFailedException(string username, string errorCode)
        : base($"Change password failed. Username: {username}, Error Code: {errorCode}")
        {
            this.Username = username;
            this.ErrorCode = errorCode;
        }

        public ChangePasswordFailedException(string username, string errorCode, Exception innerException)
            : base($"Change password failed. Username: {username}, Error Code: {errorCode}", innerException)
        {
            this.Username = username;
            this.ErrorCode = errorCode;
        }

        public string Username { get; private set; }
        public string ErrorCode { get; private set; }
    }
}

