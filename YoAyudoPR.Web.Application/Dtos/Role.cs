using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoAyudoPR.Web.Application.Dtos
{
    public class RoleListResponse
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
