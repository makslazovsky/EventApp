using Application.DTOs;
using MediatR;

namespace Application.UseCases.Events.GetEventById
{
    public class GetEventByIdQuery : IRequest<EventDto>
    {
        public Guid Id { get; set; }
    }

}
