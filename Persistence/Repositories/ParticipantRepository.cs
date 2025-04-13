using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ParticipantRepository : IParticipantRepository
{
    private readonly EventAppDbContext _context;

    public ParticipantRepository(EventAppDbContext context)
    {
        _context = context;
    }

    public async Task<Participant?> GetByIdAsync(Guid id)
    {
        return await _context.Participants
            .Include(p => p.Event)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Participant>> GetByEventIdAsync(Guid eventId)
    {
        return await _context.Participants
            .Where(p => p.EventId == eventId)
            .ToListAsync();
    }

    public async Task<List<Participant>> GetAllByEventIdAsync(Guid eventId)
    {
        return await _context.Participants
            .Where(p => p.EventId == eventId)
            .ToListAsync();
    }

    public async Task AddAsync(Participant participant)
    {
        await _context.Participants.AddAsync(participant);
    }

    public void Delete(Participant participant)
    {
        _context.Participants.Remove(participant);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
