using Application.Entities;
using MediatR;

namespace Application.UseCases.Events.GetEventById
{
    public class GetEventByIdQuery : IRequest<Event>
    {
        public Guid Id { get; set; }
    }

}
