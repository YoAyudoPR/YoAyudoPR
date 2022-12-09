using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Domain.Entities;
using static YoAyudoPR.Web.Application.Dtos.ActivityLog;

namespace YoAyudoPR.Web.Application.Services
{
    public interface IActivityLogService
    {
        Task<IEnumerable<ActivityLogListResponse>> FindAll(Expression<Func<Activitylog, bool>> predicate, CancellationToken cancellationToken);
        Task<EventResponse> FirstByConditionAsync(Expression<Func<Activitylog, bool>> predicate, CancellationToken cancellationToken);
        Task RequestPartiticpation(ActivityLogCreateRequest model, CancellationToken cancellationToken);
        Task Update(ActivityLogUpdateRequest model, CancellationToken cancellationToken);
        Task Delete(Guid guid, CancellationToken cancellationToken);
    }
}
