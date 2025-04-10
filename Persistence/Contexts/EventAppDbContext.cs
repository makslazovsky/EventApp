using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Persistence.Contexts
{
    public class EventAppDbContext : DbContext
    {
        public EventAppDbContext(DbContextOptions<EventAppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Event> Events => Set<Event>(); // Пример: если у тебя уже есть сущность Event
    }
}

