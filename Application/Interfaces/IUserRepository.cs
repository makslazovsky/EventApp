using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string username, CancellationToken cancellationToken);
        Task AddUserAsync(User user, CancellationToken cancellationToken);
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);

        Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task UpdateUserAsync(User user, CancellationToken cancellationToken);
    }
}
