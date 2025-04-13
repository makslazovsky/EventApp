using MediatR;

namespace Application.UseCases.Participants.CancelParticipantRegistration
{
    public class CancelParticipantRegistrationCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public CancelParticipantRegistrationCommand(Guid id)
        {
            Id = id;
        }
    }
}
