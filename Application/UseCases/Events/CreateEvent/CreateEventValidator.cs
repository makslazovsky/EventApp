using FluentValidation;

namespace Application.UseCases.Events.CreateEvent
{
    public class CreateEventValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.DateTime).GreaterThan(DateTime.Now).WithMessage("Date must be in the future");
            RuleFor(x => x.MaxParticipants).GreaterThan(0);
        }
    }
}
