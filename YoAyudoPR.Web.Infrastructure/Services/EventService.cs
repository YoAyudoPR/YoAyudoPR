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
    public class EventService : IEventService
    {
        private readonly IGenericRepository<Event> _eventRepository;
        private readonly IGenericRepository<Organization> _organizationRepository;

        public EventService(railwayContext context)
        {
            _eventRepository = new GenericRepository<Event>(context);
            _organizationRepository = new GenericRepository<Organization>(context);
        }

        public async Task Create(EventCreateRequest model, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.FindByGuidAsync(model.OrganizationGuid, cancellationToken);

            var newEvent = new Event
            {
                Guid = Guid.NewGuid(),
                Description = model.Description,
                Createdat = DateTime.Now,
                Startdate = model.Startdate,
                Enddate = model.Enddate,
                Capacity = model.Capacity,
                Websiteurl = model.Websiteurl,
                Address = model.Address,
                OrganizationId = organization.Id,
                CategoryId = model.CategoryId
            };

            await _eventRepository.AddAndSaveAsync(newEvent, cancellationToken);
        }

        public Task Delete(Guid guid, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EventListResponse>> FindAll(Expression<Func<Event, bool>> predicate, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.GetUsingIncludes(includes: new List<Expression<Func<Event, object>>>()
            {
                events => events.Organization,
                events => events.Category
            }, predicate, cancellationToken);

            var eventList = events.Select(x => new EventListResponse
            {
                Guid = x.Guid.GetValueOrDefault(),
                OrganizationName = x.Organization.Name,
                Startdate = x.Startdate,
                Enddate = x.Enddate,
                Address = x.Address,
                CategoryName = x.Category.Name
            });

            return eventList;
        }

        public async Task<EventResponse> FirstByConditionAsync(Expression<Func<Event, bool>> predicate, CancellationToken cancellationToken)
        {
            var dbEvent = await _eventRepository.FirstUsingIncludesAsync(includes: new List<Expression<Func<Event, object>>>()
            {
                events => events.Organization,
                events => events.Category
            }, predicate, cancellationToken);

            var eventInfo = new EventResponse
            {
                Guid = dbEvent.Guid.GetValueOrDefault(),
                OrganizationGuid = dbEvent.Organization.Guid.GetValueOrDefault(),
                OrganizationName = dbEvent.Organization.Name,
                Startdate = dbEvent.Startdate,
                Enddate = dbEvent.Enddate,
                Description = dbEvent.Description,
                Capacity = dbEvent.Capacity,
                Websiteurl = dbEvent.Websiteurl,
                Address = dbEvent.Address,
                CategoryName = dbEvent.Category.Name
            };

            return eventInfo;
        }

        public async Task Update(EventUpdateRequest model, CancellationToken cancellationToken)
        {
            var dbEvent = await _eventRepository.FirstByConditionAsync(x => x.Guid == model.Guid, cancellationToken);

            dbEvent.Description = model.Description;
            dbEvent.Startdate = model.Startdate;
            dbEvent.Enddate = model.Enddate;
            dbEvent.Capacity = model.Capacity;
            dbEvent.Websiteurl = model.Websiteurl;
            dbEvent.Address = model.Address;
            dbEvent.CategoryId = model.CategoryId;

            await _eventRepository.UpdateAndSaveAsync(dbEvent, cancellationToken);
        }
    }
}
