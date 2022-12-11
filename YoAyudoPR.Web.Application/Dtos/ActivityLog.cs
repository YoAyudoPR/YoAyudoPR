using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoAyudoPR.Web.Application.Dtos
{
    public class ActivityLogCreateRequest
    {
        [Required]
        public Guid EventGuid { get; set; }
        [Required]
        public Guid UserGuid { get; set; }
    }

    public class ActivityLogUpdateRequest
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public int HoursVolunteered { get; set; }
    }

    public class ActivityLogResponse
    {
        public Guid? Guid { get; set; }
        public Guid UserGuid { get; set; }
        public string? UserName { get; set; }
        public string? OrganizationName { get; set; }
        public Guid EventGuid { get; set; }
        public string? EventName { get; set; }
        public int? Hoursvolunteered { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public string? Status { get; set; }
    }
}
