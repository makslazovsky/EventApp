using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Participants.GetParticipantById
{
    public class GetParticipantByIdQueryHandler : IRequestHandler<GetParticipantByIdQuery, ParticipantDto>
    {
        private readonly IParticipantRepository _repository;
        private readonly IMapper _mapper;

        public GetParticipantByIdQueryHandler(IParticipantRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ParticipantDto> Handle(GetParticipantByIdQuery request, CancellationToken cancellationToken)
        {
            var participant = await _repository.GetByIdAsync(request.Id);

            if (participant is null)
                throw new NotFoundException(nameof(Participant), request.Id);

            return _mapper.Map<ParticipantDto>(participant);
        }
    }
}
