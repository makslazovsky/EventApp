using Application.UseCases.Participants.CancelParticipantRegistration;
using Application.UseCases.Participants.GetParticipantById;
using Application.UseCases.Participants.GetParticipantsByEventId;
using Application.UseCases.Participants.RegisterParticipant;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParticipantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Register([FromBody] RegisterParticipantCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var participant = await _mediator.Send(new GetParticipantByIdQuery(id));
            return Ok(participant);
        }

        [HttpGet("event/{eventId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByEventId(Guid eventId)
        {
            var participants = await _mediator.Send(new GetParticipantsByEventIdQuery(eventId));
            return Ok(participants);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _mediator.Send(new CancelParticipantRegistrationCommand(id));
            return NoContent();
        }
    }
}
