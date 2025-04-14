using Application.DTOs;
using MediatR;

namespace Application.UseCases.Users.LoginUser
{
    public class LoginUserCommand : IRequest<AuthResponseDto>
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
