using DomoNow.Communications.Domain.Entities;

namespace DomoNow.Communications.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(Guid Id);
        Task Update(User user);
        Task DeleteById(Guid Id);
        Task<IReadOnlyList<User>> GetAll();
        Task Create(User user);
    }
}
