using Application.DTOs;
using Application.Entities;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Events.GetEventByTitle
{
    public class GetEventByTitleHandler : IRequestHandler<GetEventByTitleQuery, EventDto>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public GetEventByTitleHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventDto> Handle(GetEventByTitleQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetByTitleAsync(request.Title)
                          ?? throw new NotFoundException(nameof(Event), request.Title);

            return _mapper.Map<EventDto>(@event);
        }
    }
}
