using MediatR;

namespace Application.UseCases.Participants.RegisterParticipant
{
    public record RegisterParticipantCommand(
     string FirstName,
     string LastName,
     DateTime BirthDate,
     string Email,
     Guid EventId
    ) : IRequest<Guid>;

}
