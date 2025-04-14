using Application.DTOs;
using MediatR;

namespace Application.UseCases.Users.RefreshToken
{
    public class RefreshTokenCommand : IRequest<AuthResponseDto>
    {
        public string RefreshToken { get; set; } = default!;
    }
}
