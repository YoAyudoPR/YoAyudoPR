using System;
namespace YoAyudoPR.Web.Application.Exceptions
{
	public class ChangeSecurityQuestionFailedException : Exception
	{
        public ChangeSecurityQuestionFailedException(string username, int questionId, string errorCode)
        : base($"Change security question failed. Username: {username}, Question Id: {questionId}, Error Code: {errorCode}")
        {
            this.Username = username;
            this.QuestionId = questionId;
            this.ErrorCode = errorCode;
        }

        public ChangeSecurityQuestionFailedException(string username, int questionId, string errorCode, Exception innerException)
            : base($"Change security question. Username: {username}, Question Id: {questionId}, Error Code: {errorCode}", innerException)
        {
            this.Username = username;
            this.QuestionId = questionId;
            this.ErrorCode = errorCode;
        }

        public string Username { get; private set; }
        public int QuestionId { get; private set; }
        public string ErrorCode { get; private set; }
    }
}

