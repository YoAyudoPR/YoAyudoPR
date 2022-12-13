using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoAyudoPR.Web.Application.Dtos
{
    public class EventSearchRequest
    {
        [Required]
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
    }

    public class EventCreateRequest
    {
        [Required]
        public Guid OrganizationGuid { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime? Startdate { get; set; }

        public DateTime? Enddate { get; set; }

        public int? Capacity { get; set; }

        public string? Websiteurl { get; set; }

        public string? Address { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }

    public class EventUpdateRequest
    {
        public Guid Guid { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime? Startdate { get; set; }

        public DateTime? Enddate { get; set; }

        public int? Capacity { get; set; }

        public string? Websiteurl { get; set; }

        public string? Address { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }

    public class EventResponse
    {
        public Guid Guid { get; set; }
        public Guid OrganizationGuid { get; set; }
        public string? OrganizationName { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public int? Capacity { get; set; }
        public string? Websiteurl { get; set; }
        public string? Address { get; set; }
        public string? CategoryName { get; set; }
    }

    public class EventListResponse
    {
        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public string? OrganizationName { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public string? Address { get; set; }
        public string? CategoryName { get; set; }
    }

}
