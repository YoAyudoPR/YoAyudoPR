using System;
using System.Collections.Generic;

namespace YouAyudoPR.Web.Domain.Entities
{
    public partial class Role
    {
        public Role()
        {
            Members = new HashSet<Member>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Member> Members { get; set; }
    }
}
