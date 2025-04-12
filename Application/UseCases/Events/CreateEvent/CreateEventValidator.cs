using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
