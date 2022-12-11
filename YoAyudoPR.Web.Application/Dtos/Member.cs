using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoAyudoPR.Web.Application.Dtos
{
    public class MemberCreateRequest
    {
        [Required]
        public Guid UserGuid { get; set; }
        [Required]
        public Guid OrganizationGuid { get; set; }
        [Required]
        public int RoleId { get; set; }
    }

    public class MemberUpdateRequest
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public int RoleId { get; set; }
    }

    public class MemberResponse
    {
        public Guid UserGuid { get; set; }
        public Guid OrganizationGuid { get; set; }
        public string? OrganizationName { get; set; }
        public string? RoleName { get; set; }
    }
}
