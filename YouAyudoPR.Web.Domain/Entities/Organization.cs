using System;
using System.Collections.Generic;

namespace YouAyudoPR.Web.Domain.Entities
{
    public partial class Organization
    {
        public Organization()
        {
            Events = new HashSet<Event>();
            Members = new HashSet<Member>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool? Isactive { get; set; }
        public bool Isdeleted { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
