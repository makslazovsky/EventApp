﻿using Application.Interfaces;
using MediatR;

namespace Application.UseCases.Events.UpdateEvent
{
    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, Unit>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var entity = await _eventRepository.GetByIdAsync(request.Id);
            if (entity == null)
                throw new Exception("Event not found");

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Date = request.DateTime;
            entity.Location = request.Location;
            entity.Category = request.Category;
            entity.MaxParticipants = request.MaxParticipants;

            await _eventRepository.UpdateAsync(entity);

            return Unit.Value;
        }
    }
}
