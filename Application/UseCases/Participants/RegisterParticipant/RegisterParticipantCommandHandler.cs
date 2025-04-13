using Application.Exceptions;
using Application.Interfaces;
using Application.UseCases.Participants.RegisterParticipant;
using Domain.Entities;
using MediatR;

public class RegisterParticipantCommandHandler : IRequestHandler<RegisterParticipantCommand, Guid>
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IEventRepository _eventRepository;

    public RegisterParticipantCommandHandler(
        IParticipantRepository participantRepository,
        IEventRepository eventRepository)
    {
        _participantRepository = participantRepository;
        _eventRepository = eventRepository;
    }

    public async Task<Guid> Handle(RegisterParticipantCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _eventRepository.GetByIdWithParticipantsAsync(request.EventId);

        if (eventEntity is null)
            throw new NotFoundException(nameof(Event), request.EventId);

        if (eventEntity.Participants.Count >= eventEntity.MaxParticipants)
            throw new InvalidOperationException("Максимальное количество участников достигнуто.");

        var participant = new Participant
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            Email = request.Email,
            RegistrationDate = DateTime.UtcNow,
            EventId = request.EventId
        };

        await _participantRepository.AddAsync(participant);
        await _participantRepository.SaveChangesAsync();

        return participant.Id;
    }
}
