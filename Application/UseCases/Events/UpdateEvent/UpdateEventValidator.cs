﻿using FluentValidation;

namespace Application.UseCases.Events.UpdateEvent
{
    public class UpdateEventValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.DateTime).GreaterThan(DateTime.Now);
            RuleFor(x => x.MaxParticipants).GreaterThan(0);
        }
    }
}
