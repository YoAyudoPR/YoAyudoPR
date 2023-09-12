namespace YoAyudoPR.Web.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid userGuid, Guid correlationId)
        : base($"User was not found. User Guid: {userGuid} | Correlation Id: {correlationId}")
        {
            this.UserGuid = userGuid;
            this.CorrelationId = correlationId;
            this.ErrorMessage = "User was not found";
        }

        public Guid UserGuid { get; private set; }
        public Guid CorrelationId { get; private set; }
        public string ErrorMessage { get; private set; }
    }
}

