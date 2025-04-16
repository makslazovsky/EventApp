using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Events.GetAllEvents
{
    public class GetAllEventsHandler : IRequestHandler<GetAllEventsQuery, PagedResult<EventDto>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public GetAllEventsHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<EventDto>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var allEvents = await _eventRepository
                .GetAllWithParticipantsAsync();

            var items = allEvents
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var dtoItems = _mapper.Map<List<EventDto>>(items);

            return new PagedResult<EventDto>
            {
                Items = dtoItems,
                TotalCount = allEvents.Count()
            };
        }
    }


}
