using System;
namespace YoAyudoPR.Web.Application.Exceptions
{
	public class ChangePasswordFailedException : Exception
	{
        public ChangePasswordFailedException(string username, string errorMessage)
        : base($"Change password failed. Username: {username}, Error Code: {errorMessage}")
        {
            this.Username = username;
            this.ErrorMessage = errorMessage;
        }

        public string Username { get; private set; }
        public string ErrorMessage { get; private set; }
    }
}

