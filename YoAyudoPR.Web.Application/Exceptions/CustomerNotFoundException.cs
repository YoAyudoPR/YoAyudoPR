namespace YoAyudoPR.Web.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid userGuid, Guid correlationId)
        : base($"User was not found. User Guid: {userGuid} | Correlation Id: {correlationId}")
        {
            this.UserGuid = userGuid;
            this.CorrelationId = correlationId;
            this.ErrorCode = "Customer_Not_Found";
        }

        public Guid UserGuid { get; private set; }
        public Guid CorrelationId { get; private set; }
        public string ErrorCode { get; private set; }
    }
}

