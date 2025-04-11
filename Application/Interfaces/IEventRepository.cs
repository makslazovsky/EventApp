using Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(Guid id);
        Task<Event?> GetByTitleAsync(string title);
        Task<IEnumerable<Event>> GetByCategoryAsync(string category);
        Task<IEnumerable<Event>> GetByDateAsync(DateTime date);
        Task AddAsync(Event ev);
        Task UpdateAsync(Event ev);
        Task DeleteAsync(Event ev);
    }

}
