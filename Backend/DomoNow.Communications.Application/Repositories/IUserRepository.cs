using DomoNow.Communications.Domain.Entities;

namespace DomoNow.Communications.Application.Repositories
{
    public interface IAuthRepository
    {
        Task<User?> GetById(Guid Id);
        Task<User?> GetByEmail(string email);
        Task Update(User user);
        Task DeleteById(Guid Id);
        Task<IReadOnlyList<User>> GetAll();
        Task Create(User user);
    }
}
