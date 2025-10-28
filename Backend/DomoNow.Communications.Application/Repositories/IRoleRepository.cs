using DomoNow.Communications.Domain.Entities;

namespace DomoNow.Communications.Application.Repositories
{
    public interface IRoleRepository
    {
        Task Create(Role role);
        Task<IReadOnlyList<Role>> GetAll();
    }
}
