using Application.DTOs;
using Application.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Participants.GetParticipantById
{
    public class GetParticipantByIdQueryHandler : IRequestHandler<GetParticipantByIdQuery, ParticipantDto>
    {
        private readonly IParticipantRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetParticipantByIdQueryHandler(
            IParticipantRepository repository,
            IMapper mapper,
            ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<ParticipantDto> Handle(GetParticipantByIdQuery request, CancellationToken cancellationToken)
        {
            var participant = await _repository.GetByIdAsync(request.Id);

            if (participant is null)
                throw new NotFoundException(nameof(Participant), request.Id);

            var isAdmin = _currentUser.Role == "Admin";
            var isOwner = participant.UserId == _currentUser.UserId;

            if (!isAdmin && !isOwner)
                throw new UnauthorizedException("Вы не имеете доступа к этому участнику");

            return _mapper.Map<ParticipantDto>(participant);
        }
    }

}
