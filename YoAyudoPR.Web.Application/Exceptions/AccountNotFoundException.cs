using System;
namespace YoAyudoPR.Web.Application.Exceptions
{
	public class AccountNotFoundException : Exception
	{		
        public AccountNotFoundException(string username)
        : base($"Account was not found. Username: {username}.")
        {
            this.Username = username;
            this.ErrorCode = "Account_User_NonExistent";
        }

        public AccountNotFoundException(string username, Exception innerException)
            : base($"Account was not found. Username: {username}.", innerException)
        {
            this.Username = username;
            this.ErrorCode = "Account_User_NonExistent";
        }

        public string Username { get; private set; }
        public string ErrorCode { get; private set; }
    }
}

