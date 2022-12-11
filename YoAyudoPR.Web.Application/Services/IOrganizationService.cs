using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Domain.Entities;

namespace YoAyudoPR.Web.Application.Services
{
    public interface IOrganizationService
    {
        Task<IEnumerable<OrganizationResponse>> FindAll(Expression<Func<Organization, bool>> predicate, CancellationToken cancellationToken);
        Task<OrganizationResponse> FirstByConditionAsync(Expression<Func<Organization, bool>> predicate, CancellationToken cancellationToken);
        Task<Guid> Create(OrganizationCreateRequest model, CancellationToken cancellationToken);
        Task Update(OrganizationUpdateRequest model, CancellationToken cancellationToken);
        Task Delete(Guid guid, CancellationToken cancellationToken);
    }
}
