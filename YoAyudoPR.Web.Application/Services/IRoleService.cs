using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoAyudoPR.Web.Application.Dtos;

namespace YoAyudoPR.Web.Application.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleListResponse>> FindAll(CancellationToken cancellationToken);
    }
}
