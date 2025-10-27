using AutoMapper;
using DomoNow.Communications.Application.Dtos;
using DomoNow.Communications.Application.Repositories;
using MediatR;

namespace DomoNow.Communications.Application.UseCases.User.Queries.Handlers
{
    internal class GetAllHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<GetAll, ApiResponse<IReadOnlyList<UserDto>>>
    {
        private readonly IUserRepository _userRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ApiResponse<IReadOnlyList<UserDto>>> Handle(GetAll request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Domain.Entities.User> users = await _userRepository.GetAll();

            IReadOnlyList<UserDto> result = _mapper.Map<IReadOnlyList<UserDto>>(users);

            return ApiResponse<IReadOnlyList<UserDto>>.Success(result);

        }
    }
}
