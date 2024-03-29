﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace YoAyudoPR.Web.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            Activitylogs = new HashSet<Activitylog>();
            Members = new HashSet<Member>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Initial { get; set; }
        public string Lastname { get; set; }
        public string Secondlastname { get; set; }
        public string Phone { get; set; }
        public bool Resetpassword { get; set; }
        public bool Isdeleted { get; set; }
        public string Passwordhash { get; set; }
        public string Passwordsalt { get; set; }
        public bool? Isactive { get; set; }
        public Guid? Guid { get; set; }

        public virtual ICollection<Activitylog> Activitylogs { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}