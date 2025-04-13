using Application.DTOs;
using MediatR;

namespace Application.UseCases.Participants.GetParticipantById
{
    public class GetParticipantByIdQuery : IRequest<ParticipantDto>
    {
        public Guid Id { get; }

        public GetParticipantByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
