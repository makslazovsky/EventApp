using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(Guid id);
        Task<Event?> GetByTitleAsync(string title);

        Task<Event?> GetByIdWithParticipantsAsync(Guid id);
        Task<IEnumerable<Event>> GetByCategoryAsync(string category);
        Task<IEnumerable<Event>> GetByDateAsync(DateTime date);
        Task AddAsync(Event ev);
        Task UpdateAsync(Event ev);
        Task DeleteAsync(Event ev);

        Task<IEnumerable<Event>> GetAllWithParticipantsAsync();
    }

}
