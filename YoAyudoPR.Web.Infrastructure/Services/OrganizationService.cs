using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Domain.Entities;
using YoAyudoPR.Web.Domain.Repositories;
using YoAyudoPR.Web.Infrastructure.Repositories;

namespace YoAyudoPR.Web.Infrastructure.Services
{
    public class OrganizationService: IOrganizationService
    {
        private readonly IGenericRepository<Organization> _organizationRepository;

        public OrganizationService(railwayContext context)
        {
            _organizationRepository = new GenericRepository<Organization>(context);
        }

        public async Task<Guid> Create(OrganizationCreateRequest model, CancellationToken cancellationToken)
        {
            var newOrganization = new Organization
            {
                Guid = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Isactive = true,
                Isdeleted = false
            };

            await _organizationRepository.AddAndSaveAsync(newOrganization);

            return newOrganization.Guid.GetValueOrDefault();
        }

        public async Task Delete(Guid guid, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.FirstByConditionAsync(x => x.Guid == guid);

            if (organization != null)
            {
                organization.Isdeleted = true;

                await _organizationRepository.UpdateAndSaveAsync(organization);
            }
        }

        public async Task<IEnumerable<OrganizationResponse>> FindAll(Expression<Func<Organization, bool>> predicate, CancellationToken cancellationToken)
        {
            var organizations = await _organizationRepository.FindAllAsync(predicate, cancellationToken);

            var organizationList = organizations.Select(organization => new OrganizationResponse
            {
                Guid = organization.Guid.GetValueOrDefault(),
                Name = organization.Name,
                Description = organization.Description,
                Isdeleted = organization.Isdeleted,
                Isactive = organization.Isactive
            });

            return organizationList;
        }

        public async Task<OrganizationResponse> FirstByConditionAsync(Expression<Func<Organization, bool>> predicate, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.FirstByConditionAsync(predicate, cancellationToken);

            var organizationInfo = new OrganizationResponse
            {
                Guid = organization.Guid.GetValueOrDefault(),
                Name = organization.Name,
                Description = organization.Description,
                Isdeleted = organization.Isdeleted,
                Isactive = organization.Isactive
            };

            return organizationInfo;
        }

        public async Task Update(OrganizationUpdateRequest model, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.FirstByConditionAsync(x => x.Guid == model.Guid, cancellationToken);

            organization.Name = model.Name;
            organization.Description = model.Description;

            await _organizationRepository.UpdateAndSaveAsync(organization, cancellationToken);
        }
    }
}
