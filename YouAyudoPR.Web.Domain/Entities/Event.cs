using System;
using System.Collections.Generic;
using System.Collections;

namespace YouAyudoPR.Web.Domain.Entities
{
    public partial class Event
    {
        public Event()
        {
            Activitylogs = new HashSet<Activitylog>();
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string? Description { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public int? Capacity { get; set; }
        public DateTime? Createdat { get; set; }
        public BitArray Isactive { get; set; } = null!;
        public BitArray Isdeleted { get; set; } = null!;
        public string? Websiteurl { get; set; }
        public string? Address { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Organization Organization { get; set; } = null!;
        public virtual ICollection<Activitylog> Activitylogs { get; set; }
    }
}
