using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Participants.GetParticipantsByEventId;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests.UseCase.Participants
{
    public class GetParticipantsByEventIdQueryHandlerTests
    {
        private readonly Mock<IParticipantRepository> _participantRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetParticipantsByEventIdQueryHandler _handler;

        public GetParticipantsByEventIdQueryHandlerTests()
        {
            _participantRepoMock = new Mock<IParticipantRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetParticipantsByEventIdQueryHandler(_participantRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfParticipantDto_WhenParticipantsExist()
        {
            // Arrange
            var eventId = Guid.NewGuid();

            var participants = new List<Participant>
            {
                new Participant
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    BirthDate = new DateTime(1990, 1, 1),
                    Email = "ivan@example.com",
                    RegistrationDate = DateTime.UtcNow
                },
                new Participant
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Anna",
                    LastName = "Petrova",
                    BirthDate = new DateTime(1995, 5, 5),
                    Email = "anna@example.com",
                    RegistrationDate = DateTime.UtcNow
                }
            };

            var dtos = participants.Select(p => new ParticipantDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,
                Email = p.Email,
                RegistrationDate = p.RegistrationDate
            }).ToList();

            _participantRepoMock.Setup(r => r.GetByEventIdAsync(eventId))
                .ReturnsAsync(participants);

            _mapperMock.Setup(m => m.Map<List<ParticipantDto>>(participants))
                .Returns(dtos);

            var query = new GetParticipantsByEventIdQuery(eventId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Select(x => x.Email).Should().Contain(new[] { "ivan@example.com", "anna@example.com" });
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoParticipants()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var emptyParticipants = new List<Participant>();

            _participantRepoMock.Setup(r => r.GetByEventIdAsync(eventId))
                .ReturnsAsync(emptyParticipants);

            _mapperMock.Setup(m => m.Map<List<ParticipantDto>>(emptyParticipants))
                .Returns(new List<ParticipantDto>());

            var query = new GetParticipantsByEventIdQuery(eventId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
