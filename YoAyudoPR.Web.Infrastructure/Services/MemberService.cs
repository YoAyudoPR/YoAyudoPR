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
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _memberRepository;
        private readonly IGenericRepository<Organization> _organizationRepository;
        private readonly IGenericRepository<User> _userRepository;

        public MemberService(railwayContext context)
        {
            _memberRepository = new GenericRepository<Member>(context);
            _organizationRepository = new GenericRepository<Organization>(context);
            _userRepository = new GenericRepository<User>(context);
        }

        public async Task Create(MemberCreateRequest model, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstByConditionAsync(x => x.Guid == model.UserGuid, cancellationToken);
            var organization = await _organizationRepository.FirstByConditionAsync(x => x.Guid == model.OrganizationGuid, cancellationToken);

            var newMember = new Member
            {
                Guid = Guid.NewGuid(),
                UserId = user.Id,
                OrganizationId = organization.Id,
                RoleId = model.RoleId
            };

            await _memberRepository.AddAndSaveAsync(newMember, cancellationToken);
        }

        public async Task Delete(Guid guid, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.FirstByConditionAsync(x => x.Guid == guid);

            if (member != null)
            {
                await _memberRepository.RemoveAndSaveAsync(member, cancellationToken);
            }
        }

        public async Task<IEnumerable<MemberResponse>> FindAll(Expression<Func<Member, bool>> predicate, CancellationToken cancellationToken)
        {
            var members = await _memberRepository.GetUsingIncludes(includes: new List<Expression<Func<Member, object>>>()
            {
                member => member.Organization,
                member => member.User,
                member => member.Role
            }, predicate, cancellationToken);

            var memberList = members.Select(member => new MemberResponse
            {
                OrganizationGuid = member.Organization.Guid.GetValueOrDefault(),
                OrganizationName = member.Organization.Name,
                UserName = member.User.Firstname + " " + (member.User.Initial == string.Empty ? string.Empty : member.User.Initial + ". ") + member.User.Lastname + " " + member.User.Secondlastname,
                UserGuid = member.User.Guid.GetValueOrDefault(),
                RoleName = member.Role.Name
            });

            return memberList;
        }

        public async Task<MemberResponse> FirstByConditionAsync(Expression<Func<Member, bool>> predicate, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.FirstUsingIncludesAsync(includes: new List<Expression<Func<Member, object>>>()
            {
                member => member.Organization,
                member => member.User,
                member => member.Role
            }, predicate, cancellationToken);

            var memberInfo = new MemberResponse
            {
                OrganizationGuid = member.Organization.Guid.GetValueOrDefault(),
                OrganizationName = member.Organization.Name,
                UserGuid = member.User.Guid.GetValueOrDefault(),
                UserName = member.User.Firstname + " " + (member.User.Initial == string.Empty ? string.Empty : member.User.Initial + ". ") + member.User.Lastname + " " + member.User.Secondlastname,
                RoleName = member.Role.Name
            };

            return memberInfo;
        }

        public async Task Update(MemberUpdateRequest model, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.FirstByConditionAsync(x => x.Guid == model.Guid, cancellationToken);

            member.RoleId = model.RoleId;

            await _memberRepository.UpdateAndSaveAsync(member, cancellationToken);
        }
    }
}
