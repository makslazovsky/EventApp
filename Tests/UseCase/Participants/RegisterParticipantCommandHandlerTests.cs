using Domain.Interfaces.Repository;
using Application.UseCases.Participants.RegisterParticipant;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Domain.Interfaces.Services;

namespace Tests.UseCase.Participants;

public class RegisterParticipantCommandHandlerTests
{
    private readonly Mock<IParticipantRepository> _participantRepoMock = new();
    private readonly Mock<IEventRepository> _eventRepoMock = new();
    private readonly Mock<ICurrentUserService> _currentUserMock;
    private readonly RegisterParticipantCommandHandler _handler;

    public RegisterParticipantCommandHandlerTests()
    {
        _currentUserMock = new Mock<ICurrentUserService>();
        _handler = new RegisterParticipantCommandHandler(_participantRepoMock.Object, _eventRepoMock.Object, _currentUserMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldRegisterParticipant_WhenValidCommand()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        _currentUserMock.Setup(s => s.UserId).Returns(userId);
        var command = new RegisterParticipantCommand(
            FirstName: "John",
            LastName: "Doe",
            BirthDate: new DateTime(1995, 5, 1),
            Email: "john@example.com",
            EventId: eventId
        );

        _eventRepoMock
            .Setup(repo => repo.GetByIdWithParticipantsAsync(eventId))
            .ReturnsAsync(new Domain.Entities.Event
            {
                Id = eventId,
                Title = "Event 1",
                Description = "Desc",
                Date = DateTime.Now,
                Location = "Here",
                MaxParticipants = 100,
                Participants = new List<Participant>()
            });

        _participantRepoMock
            .Setup(repo => repo.AddAsync(It.IsAny<Participant>()))
            .Returns(Task.CompletedTask);

        _participantRepoMock
            .Setup(repo => repo.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty(); // Guid не должен быть пустым
        _participantRepoMock.Verify(repo => repo.AddAsync(It.IsAny<Participant>()), Times.Once);
        _participantRepoMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }


}
