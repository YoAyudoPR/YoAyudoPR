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
    public interface IMemberService
    {
        Task<IEnumerable<MemberResponse>> FindAll(Expression<Func<Member, bool>> predicate, CancellationToken cancellationToken);
        Task<MemberResponse> FirstByConditionAsync(Expression<Func<Member, bool>> predicate, CancellationToken cancellationToken);
        Task Create(MemberCreateRequest model, CancellationToken cancellationToken);
        Task Update(MemberUpdateRequest model, CancellationToken cancellationToken);
        Task Delete(Guid guid, CancellationToken cancellationToken);
    }
}
