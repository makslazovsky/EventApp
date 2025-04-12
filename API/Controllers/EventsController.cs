using Application.UseCases.Events.CreateEvent;
using Application.UseCases.Events.GetAllEvents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = result }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllEventsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
