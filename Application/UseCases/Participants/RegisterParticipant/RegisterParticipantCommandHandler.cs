using Application.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Application.UseCases.Participants.RegisterParticipant;
using Domain.Entities;
using MediatR;

public class RegisterParticipantCommandHandler : IRequestHandler<RegisterParticipantCommand, Guid>
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IEventRepository _eventRepository;
    private readonly ICurrentUserService _currentUserService;
    public RegisterParticipantCommandHandler(
    IParticipantRepository participantRepository,
    IEventRepository eventRepository,
    ICurrentUserService currentUserService)
    {
        _participantRepository = participantRepository;
        _eventRepository = eventRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(RegisterParticipantCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _eventRepository.GetByIdWithParticipantsAsync(request.EventId);

        if (eventEntity is null)
            throw new NotFoundException(nameof(Event), request.EventId);

        if (eventEntity.Participants.Count >= eventEntity.MaxParticipants)
            throw new InvalidOperationException("Максимальное количество участников достигнуто.");

        var userId = _currentUserService.UserId
             ?? throw new UnauthorizedAccessException("Пользователь не авторизован");

        if (await _participantRepository.IsUserRegisteredForEvent(userId, request.EventId))
            throw new InvalidOperationException("Вы уже зарегистрированы на это событие.");

        var participant = new Participant
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            Email = request.Email,
            RegistrationDate = DateTime.UtcNow,
            EventId = request.EventId,
            UserId = userId
        };


        await _participantRepository.AddAsync(participant);
        await _participantRepository.SaveChangesAsync();

        return participant.Id;
    }
}
