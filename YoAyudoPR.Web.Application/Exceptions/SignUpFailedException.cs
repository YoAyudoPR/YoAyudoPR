using System;
namespace YoAyudoPR.Web.Application.Exceptions
{
	public class SignUpFailedException : Exception
	{
        public SignUpFailedException(string emailAddress, string errorCode)
            : base($"Sign up failed. Email used: {emailAddress}.")
        {            
            EmailAddress = emailAddress;
            ErrorCode = errorCode;
        }

        public SignUpFailedException(string emailAddress, string errorCode, Exception innerException)
            : base($"Sign up failed. Email used: {emailAddress}.", innerException)
        {            
            EmailAddress = emailAddress;
            ErrorCode = errorCode;
        }
        
        public string EmailAddress { get; private set; }
        public string ErrorCode { get; private set; }
    }
}

