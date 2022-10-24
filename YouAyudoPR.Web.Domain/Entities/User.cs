using System;
using System.Collections.Generic;

namespace YouAyudoPR.Web.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            Activitylogs = new HashSet<Activitylog>();
            Members = new HashSet<Member>();
        }

        public int Id { get; set; }
        public string? Email { get; set; }
        public string Firstname { get; set; } = null!;
        public string? Initial { get; set; }
        public string Lastname { get; set; } = null!;
        public string? Secondlastname { get; set; }
        public string? Phone { get; set; }
        public bool Resetpassword { get; set; }
        public bool Isdeleted { get; set; }

        public virtual ICollection<Activitylog> Activitylogs { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
