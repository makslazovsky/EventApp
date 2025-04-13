using Application.Entities;
using Application.Interfaces;
using Application.UseCases.Events.GetEventById;
using MediatR;

namespace Application.UseCases.Events.GetEventByTitle
{
    public class GetEventByTitleHandler : IRequestHandler<GetEventByTitleQuery, Event>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventByTitleHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Event> Handle(GetEventByTitleQuery request, CancellationToken cancellationToken)
        {
            var result = await _eventRepository.GetByTitleAsync(request.Title);
            return result;
        }
    }
}
