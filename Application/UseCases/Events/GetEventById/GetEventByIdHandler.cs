using Application.DTOs;
using Domain.Entities;
using Application.Exceptions;
using Domain.Interfaces.Repository;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Events.GetEventById
{
    public class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, EventDto>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public GetEventByIdHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetByIdAsync(request.Id)
                      ?? throw new NotFoundException(nameof(Event), request.Id);

            return _mapper.Map<EventDto>(@event);
        }
    }
    
}
