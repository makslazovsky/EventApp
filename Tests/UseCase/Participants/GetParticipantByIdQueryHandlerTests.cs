using Application.DTOs;
using Application.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Application.UseCases.Participants.GetParticipantById;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Tests.UseCase.Participants
{
    public class GetParticipantByIdQueryHandlerTests
    {
        private readonly Mock<IParticipantRepository> _participantRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICurrentUserService> _currentUserMock;
        private readonly GetParticipantByIdQueryHandler _handler;

        public GetParticipantByIdQueryHandlerTests()
        {
            _participantRepoMock = new Mock<IParticipantRepository>();
            _mapperMock = new Mock<IMapper>();
            _currentUserMock = new Mock<ICurrentUserService>();

            _handler = new GetParticipantByIdQueryHandler(
                _participantRepoMock.Object,
                _mapperMock.Object,
                _currentUserMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnParticipantDto_WhenParticipantExistsAndUserIsOwner()
        {
            // Arrange
            var participantId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var birthDate = new DateTime(1995, 1, 1);
            var regDate = DateTime.UtcNow;

            var participant = new Participant
            {
                Id = participantId,
                UserId = userId,
                FirstName = "Ivan",
                LastName = "Ivanov",
                BirthDate = birthDate,
                Email = "ivan@example.com",
                RegistrationDate = regDate
            };

            var dto = new ParticipantDto
            {
                Id = participantId,
                FirstName = "Ivan",
                LastName = "Ivanov",
                BirthDate = birthDate,
                Email = "ivan@example.com",
                RegistrationDate = regDate
            };

            _participantRepoMock
                .Setup(r => r.GetByIdAsync(participantId))
                .ReturnsAsync(participant);

            _currentUserMock
                .Setup(c => c.UserId)
                .Returns(userId);

            _currentUserMock
                .Setup(c => c.Role)
                .Returns("User");

            _mapperMock
                .Setup(m => m.Map<ParticipantDto>(participant))
                .Returns(dto);

            var query = new GetParticipantByIdQuery(participantId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(participantId);
            result.FirstName.Should().Be("Ivan");
            result.LastName.Should().Be("Ivanov");
            result.BirthDate.Should().Be(birthDate);
            result.Email.Should().Be("ivan@example.com");
            result.RegistrationDate.Should().Be(regDate);
        }


        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenParticipantNotFound()
        {
            // Arrange
            var participantId = Guid.NewGuid();

            _participantRepoMock
                .Setup(r => r.GetByIdAsync(participantId))
                .ReturnsAsync((Participant?)null);

            var query = new GetParticipantByIdQuery(participantId);

            // Act
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage($"Entity \"Participant\" ({participantId}) not found");
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenUserNotOwnerOrAdmin()
        {
            // Arrange
            var participantId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var currentUserId = Guid.NewGuid();

            var participant = new Participant
            {
                Id = participantId,
                UserId = userId
            };

            _participantRepoMock
                .Setup(r => r.GetByIdAsync(participantId))
                .ReturnsAsync(participant);

            _currentUserMock
                .Setup(c => c.UserId)
                .Returns(currentUserId);

            _currentUserMock
                .Setup(c => c.Role)
                .Returns("User");

            var query = new GetParticipantByIdQuery(participantId);

            // Act
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<UnauthorizedException>()
                .WithMessage("Вы не имеете доступа к этому участнику");
        }
    }
}
