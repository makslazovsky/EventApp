using FluentValidation;

namespace Application.UseCases.Participants.RegisterParticipant
{
    public class RegisterParticipantCommandValidator : AbstractValidator<RegisterParticipantCommand>
    {
        public RegisterParticipantCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.BirthDate).LessThan(DateTime.Today);
            RuleFor(x => x.EventId).NotEmpty();
        }
    }

}
