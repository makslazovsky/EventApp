using Application.DTOs;
using MediatR;

namespace Application.UseCases.Events.GetFilteredEvents
{
    public class GetFilteredEventsQuery : IRequest<List<EventDto>>
    {
        public DateTime? Date { get; set; }
        public string? Location { get; set; }
        public string? Category { get; set; }
    }
}
