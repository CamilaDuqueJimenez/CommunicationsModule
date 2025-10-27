using DomoNow.Communications.Application.Services;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace DomoNow.Communications.Infrastructure.Persistence.Services
{
    public class Password : IPassword
    {
        private readonly int _BitHash = 32;
        private readonly int _DegreeOfParallelism = 4;
        private readonly int _MemorySize = 65536;
        private readonly int _Iterations = 4;
        private readonly int _SaltBytes = 16;
        public async Task<string> Hash(string password)
        {
            byte[] salt = await Salt();
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = _DegreeOfParallelism,
                MemorySize = _MemorySize,
                Iterations = _Iterations
            };
            string hash = Convert.ToBase64String(argon2.GetBytes(_BitHash));
            return await Task.FromResult(string.Join("-", _BitHash, Convert.ToBase64String(salt), hash, _MemorySize, _Iterations, _SaltBytes, _DegreeOfParallelism));
        }
        public async Task<bool> Verify(string password, string hash)
        {
            string[] parts = hash.Split('-', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 7)
            {
                throw new FormatException("Se presento un error al intentar verificar la contraseña");
            }
            int bitHash = int.Parse(parts[0]);
            string salt = parts[1];
            string hashPassword = parts[2];
            int memorySize = int.Parse(parts[3]);
            int iterations = int.Parse(parts[4]);
            int saltBytes = int.Parse(parts[5]);
            int degreeOfParallelism = int.Parse(parts[6]);
            var saltDecrypt = Convert.FromBase64String(salt);
            var originalHash = Convert.FromBase64String(hashPassword);
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = saltDecrypt,
                DegreeOfParallelism = degreeOfParallelism,
                MemorySize = memorySize,
                Iterations = iterations
            };
            var newHash = argon2.GetBytes(bitHash);
            bool match = CryptographicOperations.FixedTimeEquals(newHash, originalHash);
            return await Task.FromResult(match);
        }
        private Task<byte[]> Salt()
        {
            return Task.FromResult(RandomNumberGenerator.GetBytes(_SaltBytes));
        }
    }
}
