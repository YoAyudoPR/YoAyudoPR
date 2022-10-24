using System;
using System.Collections.Generic;

namespace YouAyudoPR.Web.Domain.Entities
{
    public partial class Activitylog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int? Hoursvolunteered { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public string? Status { get; set; }

        public virtual Event Event { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
