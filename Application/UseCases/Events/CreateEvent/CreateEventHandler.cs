using Application.Interfaces;
using MediatR;
using Application.Entities;

namespace Application.UseCases.Events.CreateEvent
{
    public class CreateEventHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var newEvent = new Event
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Date = request.DateTime,
                Location = request.Location,
                Category = request.Category,
                MaxParticipants = request.MaxParticipants,
            };

            await _eventRepository.AddAsync(newEvent);

            return newEvent.Id;
        }
    }
}
