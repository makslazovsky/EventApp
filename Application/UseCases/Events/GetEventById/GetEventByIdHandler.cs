using Application.Entities;
using Application.Interfaces;
using MediatR;

namespace Application.UseCases.Events.GetEventById
{
    public class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, Event>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventByIdHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _eventRepository.GetByIdAsync(request.Id);
            return result;
        }
    }
}
