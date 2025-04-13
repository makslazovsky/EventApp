using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Participants.GetParticipantsByEventId
{
    public class GetParticipantsByEventIdQueryHandler : IRequestHandler<GetParticipantsByEventIdQuery, List<ParticipantDto>>
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;

        public GetParticipantsByEventIdQueryHandler(IParticipantRepository participantRepository, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public async Task<List<ParticipantDto>> Handle(GetParticipantsByEventIdQuery request, CancellationToken cancellationToken)
        {
            var participants = await _participantRepository.GetByEventIdAsync(request.EventId);
            return _mapper.Map<List<ParticipantDto>>(participants);
        }
    }
}
