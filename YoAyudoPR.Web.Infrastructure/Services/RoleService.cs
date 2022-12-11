using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Domain.Entities;
using YoAyudoPR.Web.Domain.Repositories;
using YoAyudoPR.Web.Infrastructure.Repositories;

namespace YoAyudoPR.Web.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _roleService;

        public RoleService(railwayContext context)
        {
            _roleService = new GenericRepository<Role>(context);
        }

        public async Task<IEnumerable<RoleListResponse>> FindAll(CancellationToken cancellationToken)
        {
            var categories = await _roleService.FindAllAsync(cancellationToken);

            var categoryList = categories.Select(x => new RoleListResponse
            {
                RoleName = x.Name,
                RoleId = x.Id
            });

            return categoryList;
        }
    }
}
