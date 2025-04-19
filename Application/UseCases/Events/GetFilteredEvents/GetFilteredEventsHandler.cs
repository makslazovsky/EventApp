using Application.DTOs;
using Domain.Interfaces.Repository;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Events.GetFilteredEvents
{
    public class GetFilteredEventsHandler : IRequestHandler<GetFilteredEventsQuery, List<EventDto>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public GetFilteredEventsHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<List<EventDto>> Handle(GetFilteredEventsQuery request, CancellationToken cancellationToken)
        {
            var allEvents = await _eventRepository.GetAllAsync();

            var filtered = allEvents.AsQueryable();

            if (request.Date.HasValue)
                filtered = filtered.Where(e => e.Date.Date == request.Date.Value.Date);

            if (!string.IsNullOrEmpty(request.Location))
                filtered = filtered.Where(e => e.Location.ToLower().Contains(request.Location.ToLower()));

            if (!string.IsNullOrEmpty(request.Category))
                filtered = filtered.Where(e => e.Category.ToLower().Contains(request.Category.ToLower()));

            var result = _mapper.Map<List<EventDto>>(filtered.ToList());

            return result;
        }
    }
}
