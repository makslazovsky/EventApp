using MediatR;

namespace Application.UseCases.Events.UploadImage
{
    public class UploadEventImageCommand : IRequest<string>
    {
        public Guid EventId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public byte[] FileContent { get; set; } = Array.Empty<byte>();
    }

}
