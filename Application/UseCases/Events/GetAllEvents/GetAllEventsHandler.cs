using Application.Entities;
using Application.Interfaces;
using MediatR;


namespace Application.UseCases.Events.GetAllEvents
{
    public class GetAllEventsHandler : IRequestHandler<GetAllEventsQuery, List<Event>>
    {
        private readonly IEventRepository _eventRepository;

        public GetAllEventsHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
           var result = await _eventRepository.GetAllAsync();
           return result.ToList();
        }
    }
}
