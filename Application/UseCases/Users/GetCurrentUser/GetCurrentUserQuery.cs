using Application.DTOs;
using MediatR;

namespace Application.UseCases.Users.GetCurrentUser
{
    public record GetCurrentUserQuery : IRequest<CurrentUserDto>;
}
