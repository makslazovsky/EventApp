using Application.DTOs;
using MediatR;

namespace Application.UseCases.Participants.GetParticipantsByEventId
{
    public class GetParticipantsByEventIdQuery : IRequest<List<ParticipantDto>>
    {
        public Guid EventId { get; }

        public GetParticipantsByEventIdQuery(Guid eventId)
        {
            EventId = eventId;
        }
    }
}
