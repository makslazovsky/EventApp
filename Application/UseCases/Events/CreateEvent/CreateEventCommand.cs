﻿using MediatR;

namespace Application.UseCases.Events.CreateEvent
{
    public class CreateEventCommand : IRequest<Guid> 
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
    }
}
