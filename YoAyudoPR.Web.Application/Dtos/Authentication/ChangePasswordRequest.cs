using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoAyudoPR.Web.Application.Dtos.Authentication
{
    public class ChangePasswordRequest
    {
        [Required]
        public string OldPassword { get; set; } = null!;

        [Required]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z]).*$",
        ErrorMessage = "Password must be alphanumeric, contain capital letters, have a min of 8 characters and contain 1+ special character.")]
        public string NewPassword { get; set; } = null!;
    }
}
