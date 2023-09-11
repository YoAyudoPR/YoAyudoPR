using System;
namespace YoAyudoPR.Web.Application.Exceptions
{
	public class AccountRecoveryFailedException : Exception
	{
        public AccountRecoveryFailedException(string username, string errorCode)
        : base($"Account recovery failed. Username: {username}, Error Code: {errorCode}")
        {
            this.Username = username;
            this.ErrorCode = errorCode;
        }

        public AccountRecoveryFailedException(string username, string errorCode, Exception innerException)
            : base($"Account recovery failed. Username: {username}, Error Code: {errorCode}", innerException)
        {
            this.Username = username;
            this.ErrorCode = errorCode;
        }

        public string Username { get; private set; }
        public string ErrorCode { get; private set; }
    }
}

