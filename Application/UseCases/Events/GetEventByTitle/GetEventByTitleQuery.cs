using Application.Entities;
using MediatR;

namespace Application.UseCases.Events.GetEventByTitle
{
    public class GetEventByTitleQuery : IRequest<Event>
    {
        public string Title { get; set; }
    }
}
