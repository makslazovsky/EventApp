﻿using Application.Exceptions;
using Application.UseCases.Events.CreateEvent;
using Application.UseCases.Events.DeleteEvent;
using Application.UseCases.Events.GetAllEvents;
using Application.UseCases.Events.GetEventById;
using Application.UseCases.Events.GetEventByTitle;
using Application.UseCases.Events.GetFilteredEvents;
using Application.UseCases.Events.UpdateEvent;
using Application.UseCases.Events.UploadImage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateEventCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = result }, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllEventsQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEventCommand command)
        {
            if (id != command.Id)
                throw new BadRequestException("Id в теле запроса и Id в URL не совпадают");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteEventCommand { Id = id };
            await _mediator.Send(command);
            return NoContent(); 
        }

        [HttpGet("by-id/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetEventByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("by-title/{title}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var query = new GetEventByTitleQuery { Title = title };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> Filter([FromQuery] GetFilteredEventsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("{id}/upload-image")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadImage(Guid id, IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var command = new UploadEventImageCommand
            {
                EventId = id,
                FileName = file.FileName,
                FileContent = memoryStream.ToArray()
            };

            var result = await _mediator.Send(command);
            return Ok(new { imageUrl = result });
        }

    }
}
