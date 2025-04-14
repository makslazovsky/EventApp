using MediatR;

namespace Application.UseCases.Users.RegisterUser
{
    public record RegisterUserCommand(string Username, string Email, string Password) : IRequest<string>;

}
