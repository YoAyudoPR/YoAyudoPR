using System;
using System.Collections.Generic;

namespace YouAyudoPR.Web.Domain.Entities
{
    public partial class Member
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public int RoleId { get; set; }

        public virtual Organization Organization { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
