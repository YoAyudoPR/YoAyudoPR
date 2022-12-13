using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
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
    public class ActivityLogService : IActivityLogService
    {
        private readonly IGenericRepository<Event> _eventRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Activitylog> _activityLogRepository;

        public ActivityLogService(railwayContext context)
        {
            _activityLogRepository = new GenericRepository<Activitylog>(context);
            _eventRepository = new GenericRepository<Event>(context);
            _userRepository = new GenericRepository<User>(context);
        }

        public async Task Delete(Guid guid, CancellationToken cancellationToken)
        {
            var activityLog = await _activityLogRepository.FirstByConditionAsync(x => x.Guid == guid);

            if (activityLog != null)
            {
                await _activityLogRepository.RemoveAndSaveAsync(activityLog);
            }
        }

        public async Task<IEnumerable<ActivityLogResponse>> FindAll(Expression<Func<Activitylog, bool>> predicate, CancellationToken cancellationToken)
        {
            var activityLogs = await _activityLogRepository.GetUsingIncludes(includes: new List<Expression<Func<Activitylog, object>>>()
            { 
                activityLogs => activityLogs.User,
                activityLogs => activityLogs.Event,
                activityLogs => activityLogs.Event.Organization
            }, predicate, cancellationToken);

            var activityLogList = activityLogs.Select(activityLog => new ActivityLogResponse
            {
                Guid = activityLog.Guid.GetValueOrDefault(),
                OrganizationName = activityLog.Event.Organization.Name,
                EventGuid = activityLog.Event.Guid.GetValueOrDefault(),
                EventName = activityLog.Event.Name,
                UserGuid = activityLog.User.Guid.GetValueOrDefault(),
                UserName = activityLog.User.Firstname + " " + (activityLog.User.Initial.IsNullOrEmpty() ? string.Empty : activityLog.User.Initial + ". ") + activityLog.User.Lastname + " " + activityLog.User.Secondlastname,
                Hoursvolunteered = activityLog.Hoursvolunteered,
                Createdat = activityLog.Createdat,
                Updatedat = activityLog.Updatedat,
                Status = activityLog.Status
            });

            return activityLogList;
        }

        public async Task<ActivityLogResponse> FirstByConditionAsync(Expression<Func<Activitylog, bool>> predicate, CancellationToken cancellationToken)
        {
            var activityLog = await _activityLogRepository.FirstUsingIncludesAsync(includes: new List<Expression<Func<Activitylog, object>>>()
            { 
                activityLogs => activityLogs.User,
                activityLog => activityLog.Event,
                activityLog => activityLog.Event.Organization
            }, predicate, cancellationToken);

            var activityLogInfo = new ActivityLogResponse
            {
                Guid = activityLog.Guid.GetValueOrDefault(),
                EventGuid = activityLog.Event.Guid.GetValueOrDefault(),
                EventName = activityLog.Event.Name,
                UserGuid = activityLog.User.Guid.GetValueOrDefault(),
                UserName = activityLog.User.Firstname + " " + (activityLog.User.Initial.IsNullOrEmpty() ? string.Empty : activityLog.User.Initial + ". ") + activityLog.User.Lastname + " " + activityLog.User.Secondlastname,
                Hoursvolunteered = activityLog.Hoursvolunteered,
                Createdat = activityLog.Createdat,
                Updatedat = activityLog.Updatedat,
                Status = activityLog.Status
            };

            return activityLogInfo;
        }

        public async Task RequestPartiticpation(ActivityLogCreateRequest model, CancellationToken cancellationToken)
        {
            var dbEvent = await _eventRepository.FirstByConditionAsync(x => x.Guid == model.EventGuid, cancellationToken);
            var user = await _userRepository.FirstByConditionAsync(x => x.Guid == model.UserGuid, cancellationToken);

            var newActivityLog = new Activitylog
            {
                Guid = Guid.NewGuid(),
                UserId = user.Id,
                EventId = dbEvent.Id,
                Hoursvolunteered = 0,
                Createdat = DateTime.Now,
                Updatedat = null,
                Status = "Requested"
            };

            await _activityLogRepository.AddAndSaveAsync(newActivityLog);
        }

        public async Task Update(ActivityLogUpdateRequest model, CancellationToken cancellationToken)
        {
            var activityLog = await _activityLogRepository.FirstByConditionAsync(x => x.Guid == model.Guid, cancellationToken);

            activityLog.Hoursvolunteered = model.HoursVolunteered;
            activityLog.Updatedat = DateTime.Now;

            await _activityLogRepository.UpdateAndSaveAsync(activityLog, cancellationToken);
        }
    }
}
