using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IParticipantRepository
{
    Task<Participant?> GetByIdAsync(Guid id);
    Task<List<Participant>> GetAllByEventIdAsync(Guid eventId);
    Task<List<Participant>> GetByEventIdAsync(Guid eventId);
    Task AddAsync(Participant participant);
    void Delete(Participant participant);
    Task SaveChangesAsync();
    Task<bool> IsUserRegisteredForEvent(Guid userId, Guid eventId);
}
