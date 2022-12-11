using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoAyudoPR.Web.Application.Dtos
{
    public class OrganizationResponse
    {
        public Guid? Guid { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Isactive { get; set; }
        public bool? Isdeleted { get; set; }
    }

    public class OrganizationCreateRequest
    {
        [Required]
        public Guid UserGuid { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
    }

    public class OrganizationUpdateRequest
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
    }
}
