using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Participants.CancelParticipantRegistration
{
    public class CancelParticipantRegistrationCommandHandler : IRequestHandler<CancelParticipantRegistrationCommand, Unit>
    {
        private readonly IParticipantRepository _repository;

        public CancelParticipantRegistrationCommandHandler(IParticipantRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CancelParticipantRegistrationCommand request, CancellationToken cancellationToken)
        {
            var participant = await _repository.GetByIdAsync(request.Id);

            if (participant is null)
                throw new NotFoundException(nameof(Participant), request.Id);

            _repository.Delete(participant);
            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
