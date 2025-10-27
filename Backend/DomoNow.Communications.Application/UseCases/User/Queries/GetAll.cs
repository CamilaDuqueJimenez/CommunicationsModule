using DomoNow.Communications.Application.Dtos;
using MediatR;

namespace DomoNow.Communications.Application.UseCases.User.Queries
{
    public record GetAll() : IRequest<ApiResponse<IReadOnlyList<UserDto>>>{}
}
