using Application.Exceptions;
using Domain.Interfaces.Repository;
using Application.UseCases.Participants.CancelParticipantRegistration;
using Domain.Entities;
using FluentAssertions;
using MediatR;
using Moq;

namespace Tests.UseCase.Participants
{
    public class CancelParticipantRegistrationCommandHandlerTests
    {
        private readonly Mock<IParticipantRepository> _participantRepoMock;
        private readonly CancelParticipantRegistrationCommandHandler _handler;

        public CancelParticipantRegistrationCommandHandlerTests()
        {
            _participantRepoMock = new Mock<IParticipantRepository>();
            _handler = new CancelParticipantRegistrationCommandHandler(_participantRepoMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteParticipant_WhenParticipantExists()
        {
            // Arrange
            var participantId = Guid.NewGuid();

            var participant = new Participant
            {
                Id = participantId,
                FirstName = "Ivan",
                LastName = "Petrov",
                Email = "ivan@example.com"
            };

            _participantRepoMock
                .Setup(repo => repo.GetByIdAsync(participantId))
                .ReturnsAsync(participant);

            _participantRepoMock
                .Setup(repo => repo.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(new CancelParticipantRegistrationCommand(participantId), CancellationToken.None);

            // Assert
            _participantRepoMock.Verify(r => r.Delete(participant), Times.Once);
            _participantRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            result.Should().Be(Unit.Value);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenParticipantDoesNotExist()
        {
            // Arrange
            var participantId = Guid.NewGuid();

            _participantRepoMock
                .Setup(repo => repo.GetByIdAsync(participantId))
                .ReturnsAsync((Participant?)null);

            var command = new CancelParticipantRegistrationCommand(participantId);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage($"Entity \"Participant\" ({participantId}) not found");
        }
    }
}
