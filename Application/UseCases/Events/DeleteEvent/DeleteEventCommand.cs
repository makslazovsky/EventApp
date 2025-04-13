using MediatR;

namespace Application.UseCases.Events.DeleteEvent
{
    public class DeleteEventCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
