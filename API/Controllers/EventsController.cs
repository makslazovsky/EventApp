using Application.UseCases.Events.CreateEvent;
using Application.UseCases.Events.DeleteEvent;
using Application.UseCases.Events.GetAllEvents;
using Application.UseCases.Events.GetEventById;
using Application.UseCases.Events.GetEventByTitle;
using Application.UseCases.Events.UpdateEvent;
using MediatR;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEventCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteEventCommand { Id = id };
            await _mediator.Send(command);
            return NoContent(); 
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetEventByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("by-title/{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var query = new GetEventByTitleQuery { Title = title };
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
