using Application.DTOs;
using Application.Exceptions;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Users.GetCurrentUser
{
    public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, CurrentUserDto>
    {
        private readonly ICurrentUserService _currentUser;

        public GetCurrentUserHandler(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        public Task<CurrentUserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            if (_currentUser.UserId == null)
                throw new UnauthorizedException("Пользователь не авторизован");

            return Task.FromResult(new CurrentUserDto
            {
                UserId = _currentUser.UserId.Value,
                Role = _currentUser.Role
            });
        }
    }
}
