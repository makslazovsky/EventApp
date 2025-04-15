using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace Application.UseCases.Users.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.UserExistsAsync(request.Username, cancellationToken))
                throw new Exception("Пользователь с таким именем уже существует.");

            if (await _userRepository.EmailExistsAsync(request.Email, cancellationToken))
                throw new Exception("Пользователь с таким email уже существует.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password)
            };

            if (await _userRepository.GetUsersCountAsync(cancellationToken) == 0)
            {
                user.Role = "Admin";
            }
            else
            {
                user.Role = "User"; 
            }

            await _userRepository.AddUserAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return "Регистрация успешна.";
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
