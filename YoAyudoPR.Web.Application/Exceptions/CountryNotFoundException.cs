using System;
namespace YoAyudoPR.Web.Application.Exceptions
{
	public class CountryNotFoundException : Exception
	{
		public CountryNotFoundException(string countryCode)
            : base($"Country not found. Country code: {countryCode}")
        {
            CountryCode = countryCode;
            this.ErrorCode = "Country_not_found";
        }



        public CountryNotFoundException(string countryCode, Exception innerException)
            : base($"Country not found. Country code: {countryCode}", innerException)
        {
            CountryCode = countryCode;
            this.ErrorCode = "Country_not_found";
        }

        public string CountryCode { get; private set; }
        public string ErrorCode { get; private set; }
    }
}

