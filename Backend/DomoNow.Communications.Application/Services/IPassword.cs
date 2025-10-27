namespace DomoNow.Communications.Application.Services
{
    public interface IPassword
    {
        Task<string> Hash(string password);
        Task<bool> Verify(string password, string hash);
    }
}
