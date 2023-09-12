using System.ComponentModel.DataAnnotations;

namespace YoAyudoPR.Web.Application.Dtos.Authentication
{
    public class ForgotPasswordRequest //: IValidatableObject
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (EmailValidator.Validate(Email) == false)
        //    {
        //        yield return new ValidationResult(
        //            $"Email Address format is not valid",
        //            new[] { nameof(Email) }
        //        );
        //    }
        //}
    }
}
