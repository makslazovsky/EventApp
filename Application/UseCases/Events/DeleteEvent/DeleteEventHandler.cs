using Domain.Entities;
using Application.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.UseCases.Events.DeleteEvent
{
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, Unit>
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var entity = await _eventRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Event), request.Id);
            
            await _eventRepository.DeleteAsync(entity);

            return Unit.Value; 
        }
    }
}
