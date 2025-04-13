using Domain.Entities;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventAppDbContext _context;

        public EventRepository(EventAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(Guid id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event?> GetByTitleAsync(string title)
        {
            return await _context.Events
                .FirstOrDefaultAsync(e => e.Title == title);
        }

        public async Task AddAsync(Event entity)
        {
            await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event entity)
        {
            _context.Events.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Event entity)
        {
            _context.Events.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetByCategoryAsync(string category)
        {
            return await _context.Events
                .Where(e => e.Category == category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetByDateAsync(DateTime date)
        {
            return await _context.Events
                .Where(e => e.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task<Event?> GetByIdWithParticipantsAsync(Guid id)
        {
            return await _context.Events
                .Include(e => e.Participants)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
