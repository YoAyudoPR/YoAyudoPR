using System;
namespace YoAyudoPR.Web.Application.Exceptions
{

    public class EmailAddressNotValidException : Exception
    {
        public EmailAddressNotValidException(string emailAddress)
        : base($"Email address not valid: {emailAddress}.")
        {
            this.EmailAddress = emailAddress;
            this.ErrorCode = "invalid_email_address";
        }

        public EmailAddressNotValidException(string emailAddress, Exception innerException)
            : base($"Email address not valid: {emailAddress}.", innerException)
        {
            this.EmailAddress = emailAddress;
            this.ErrorCode = "invalid_email_address";
        }

        public string EmailAddress { get; private set; }
        public string ErrorCode { get; private set; }
    }
}

