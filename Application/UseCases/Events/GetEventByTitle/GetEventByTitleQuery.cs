using Application.DTOs;
using MediatR;

namespace Application.UseCases.Events.GetEventByTitle
{
    public class GetEventByTitleQuery : IRequest<EventDto>
    {
        public string Title { get; set; } = string.Empty;
    }
}
