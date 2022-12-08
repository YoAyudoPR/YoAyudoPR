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
    public interface IEventService
    {
        Task<IEnumerable<EventListResponse>> FindAll(Expression<Func<Event, bool>> predicate, CancellationToken cancellationToken);
        Task<EventResponse> FirstByConditionAsync(Expression<Func<Event, bool>> predicate, CancellationToken cancellationToken);
        Task Create(EventCreateRequest model, CancellationToken cancellationToken);
        Task Update(EventUpdateRequest model, CancellationToken cancellationToken);
        Task Delete(Guid guid, CancellationToken cancellationToken);
    }
}
