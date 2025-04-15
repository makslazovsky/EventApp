using Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Tests.Repository;

public class ParticipantRepositoryTests
{
    private readonly EventAppDbContext _dbContext;
    private readonly ParticipantRepository _repository;

    public ParticipantRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<EventAppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new EventAppDbContext(options);
        _repository = new ParticipantRepository(_dbContext);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnParticipant_WhenExists()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var ev = new Domain.Entities.Event
        {
            Id = eventId,
            Title = "Test Event",
            Description = "Test Description",
            Date = DateTime.Now,
            Location = "Test Location"
        };

        await _dbContext.Events.AddAsync(ev);
        await _dbContext.SaveChangesAsync();

        var participant = new Participant
        {
            Id = Guid.NewGuid(),
            EventId = eventId,
            UserId = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com",
            BirthDate = DateTime.Today.AddYears(-20)
        };

        await _repository.AddAsync(participant);
        await _repository.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(participant.Id);

        // Assert
        result.Should().NotBeNull(); // вот здесь падало
        result!.Id.Should().Be(participant.Id);
        result.Event.Should().NotBeNull();
        result.Event!.Id.Should().Be(eventId);
    }

    [Fact]
    public async Task AddAsync_ShouldAddParticipantToDatabase()
    {
        // Arrange
        var participant = new Participant
        {
            Id = Guid.NewGuid(),
            EventId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com",
            BirthDate = DateTime.Today.AddYears(-20)
        };

        // Act
        await _repository.AddAsync(participant);
        await _repository.SaveChangesAsync();

        // Assert
        var found = await _dbContext.Participants.FindAsync(participant.Id);
        found.Should().NotBeNull();
        found!.FirstName.Should().Be("Test");
    }
}
