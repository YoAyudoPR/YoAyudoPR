using System;
namespace YoAyudoPR.Web.Application.Exceptions
{	
    public class ApplicationNotFoundException : Exception
    {
        public ApplicationNotFoundException(long loanInfoId, Guid correlationId)
        : base($"Application was not found. Loan Info Id: {loanInfoId}. Correlation Id: {correlationId}.")
        {
            this.LoanInfoId = loanInfoId;
            this.CorrelationId = correlationId;
            this.ErrorCode = "Application_Not_Found";
        }

        public ApplicationNotFoundException(long loanInfoId, Guid correlationId, Exception innerException)
            : base($"Application was not found. Loan Info Id: {loanInfoId}. Correlation Id: {correlationId}.", innerException)
        {
            this.LoanInfoId = loanInfoId;
            this.CorrelationId = correlationId;
            this.ErrorCode = "Application_Not_Found";
        }

        public long LoanInfoId { get; private set; }
        public Guid CorrelationId { get; private set; }
        public string ErrorCode { get; private set; }
    }
}

