﻿using Application.Exceptions;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.UseCases.Events.UploadImage
{
    public class UploadEventImageHandler : IRequestHandler<UploadEventImageCommand, string>
    {
        private readonly IEventRepository _eventRepository;

        public UploadEventImageHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<string> Handle(UploadEventImageCommand request, CancellationToken cancellationToken)
        {
            var eventItem = await _eventRepository.GetByIdAsync(request.EventId);
            if (eventItem == null)
                throw new NotFoundException("Event ", request.EventId);

            var ext = Path.GetExtension(request.FileName);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fullPath = Path.Combine(folderPath, fileName);
            await File.WriteAllBytesAsync(fullPath, request.FileContent, cancellationToken);

            eventItem.ImagePath = $"/images/{fileName}";
            await _eventRepository.UpdateAsync(eventItem);

            return eventItem.ImagePath!;
        }
    }

}
