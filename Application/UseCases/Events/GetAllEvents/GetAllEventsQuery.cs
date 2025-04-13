using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.UseCases.Events.GetAllEvents
{
    public record GetAllEventsQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PagedResult<EventDto>>;

}
