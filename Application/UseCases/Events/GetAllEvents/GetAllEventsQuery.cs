using Application.Entities;
using MediatR;

namespace Application.UseCases.Events.GetAllEvents
{
    public record GetAllEventsQuery() : IRequest<List<Event>>;
}
