using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoAyudoPR.Web.Application.Dtos
{
    public class UserSearchRequest
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
    }

    public class UserCreateRequest
    {
        [Required]
        [MaxLength(150)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; } = null!;

        [MaxLength(1)]
        public string? Initial { get; set; }

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; } = null!;

        [MaxLength(40)]
        public string? SecondLastName { get; set; }

        public string? Phone { get; set; }

        [Required]
        public string Password { get; set; } = null!;
    }

    public class UserUpdateRequest
    {
        [Required]
        public Guid Guid { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; } = null!;

        [MaxLength(1)]
        public string? Initial { get; set; }

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; } = null!;

        [MaxLength(40)]
        public string? SecondLastName { get; set; }

        public string? Phone { get; set; }
        public string? Password { get; set; }
    }

    public class UserResponse
    {
        public Guid Guid { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? Initial { get; set; }
        public string? LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string? Phone { get; set; }
        public bool? ResetPassword { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }
}
