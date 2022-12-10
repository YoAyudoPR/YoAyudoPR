using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoAyudoPR.Web.Application.Dtos
{
    public class CategoryListResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
